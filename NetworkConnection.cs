﻿// <copyright file="NetworkConnection.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Sockets;
using System.Text;
namespace CS3500.Networking;

/// <summary>
///   Wraps the StreamReader/Writer/TcpClient together so we
///   don't have to keep creating all three for network actions.
/// </summary>
public sealed class NetworkConnection : IDisposable
{
    /// <summary>
    ///   The connection/socket abstraction
    /// </summary>
    private TcpClient _tcpClient = new();

    /// <summary>
    ///   Reading end of the connection
    /// </summary>
    private StreamReader? _reader = null;

    /// <summary>
    ///   Writing end of the connection
    /// </summary>
    private StreamWriter? _writer = null;

    /// <summary>
    /// Log information
    /// </summary>
    private readonly ILogger _logger = NullLogger.Instance;

    /// <summary>
    ///   Initializes a new instance of the <see cref="NetworkConnection"/> class.
    ///   <para>
    ///     Create a network connection object.
    ///   </para>
    /// </summary>
    /// <param name="tcpClient">
    ///   An already existing TcpClient
    /// </param>
    /// <param name="logger"></param>
    public NetworkConnection(TcpClient tcpClient, ILogger logger)
    {
        _tcpClient = tcpClient;
        _logger = logger;
        if (IsConnected)
        {
            // Only establish the reader/writer if the provided TcpClient is already connected.
            _reader = new StreamReader(_tcpClient.GetStream(), new UTF8Encoding(false));
            _writer = new StreamWriter(_tcpClient.GetStream(), new UTF8Encoding(false)) { AutoFlush = true }; // AutoFlush ensures data is sent immediately

        }
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="NetworkConnection"/> class.
    ///   <para>
    ///     Create a network connection object. The tcpClient will be unconnected at the start.
    ///   </para>
    /// </summary>
    public NetworkConnection(ILogger logger)
        : this(new TcpClient(), logger)
    {
    }

    /// <summary>
    /// Gets a value indicating whether the socket is connected.
    /// </summary>
    public bool IsConnected
    {
        get
        {
            bool connected = _tcpClient.Connected;
            if (connected)
                _logger.LogInformation("Network is connected");
            return connected;
        }
    }


    /// <summary>
    ///   Try to connect to the given host:port. 
    /// </summary>
    /// <param name="host"> The URL or IP address, e.g., www.cs.utah.edu, or  127.0.0.1. </param>
    /// <param name="port"> The port, e.g., 11000. </param>
    public void Connect(string host, int port)
    {
        try
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(host, port);
            _logger.LogInformation($"{port} connected from {host}");

            _reader = new StreamReader(_tcpClient.GetStream(), new UTF8Encoding(false));
            _writer = new StreamWriter(_tcpClient.GetStream(), new UTF8Encoding(false)) { AutoFlush = true };

        }
        catch (Exception )
        {
            _logger.LogError($"{port} failed to connect from {host}");

        }

    }


    /// <summary>
    ///   Send a message to the remote server.  If the <paramref name="message"/> contains
    ///   new lines, these will be treated on the receiving side as multiple messages.
    ///   This method should attach a newline to the end of the <paramref name="message"/>
    ///   (by using WriteLine).
    ///   If this operation can not be completed (e.g. because this NetworkConnection is not
    ///   connected), throw an InvalidOperationException.
    /// </summary>
    /// <param name="message"> The string of characters to send. </param>
    public void Send(string message)
    {
        if (!IsConnected || _writer == null)
        {
            _logger.LogError("Attempted to send message while disconnected.");
            return;
        }
        try
        {
            if (message != string.Empty || message != null)
            {
                _writer.WriteLine(message);
                _logger.LogInformation("Sent message: {Message}", message);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send message.");
            throw new InvalidOperationException("Error sending data", ex);
        }
    }


    /// <summary>
    ///   Read a message from the remote side of the connection.  The message will contain
    ///   all characters up to the first new line. See <see cref="Send"/>.
    ///   If this operation can not be completed (e.g. because this NetworkConnection is not
    ///   connected), throw an InvalidOperationException.
    /// </summary>
    /// <returns> The contents of the message. </returns>
    public string ReadLine()
    {
        try
        {
            string? line = _reader?.ReadLine() ?? string.Empty;
            if (line != string.Empty)
                _logger.LogInformation("Received message: {Message}", line);
            return line;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read message.");
            throw new InvalidOperationException("Cannot read a message from the remote side of the connection, ex", ex);
        }

    }

    /// <summary>
    ///   If connected, disconnect the connection and clean 
    ///   up (dispose) any streams.
    /// </summary>
    public void Disconnect()
    {
        if (IsConnected)
        {
            try
            {
                _logger.LogInformation("Disconnecting from remote host.");
                _reader?.Dispose();
                _writer?.Dispose();
                _tcpClient?.Close();
                _logger.LogInformation("Disconnect Successfully");
            }
            catch
            {
                _logger.LogError("Could not disconnect");
            }
        }
    }

    /// <summary>
    ///   Automatically called with a using statement (see IDisposable)
    /// </summary>
    public void Dispose()
    {
        Disconnect();
        _logger.LogInformation("Disposed NetworkConnection resources.");

    }
}
