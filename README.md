```
Author:      Bao Phuc Do 
Partner:     Khang Hoang Nguyen
Start Date:  11-15-2024
Course:      CS 3500, University of Utah, School of Computing
GitHub ID:   assignment-eight-chatting-phuc-khang_game
Repo:        https://github.com/uofu-cs3500-20-fall2024/assignment-eight-chatting-phuc-khang_game
Commit Date: 12-05-2024    19:00
Solution:    Chatting
Copyright:   CS 3500 and Bao Phuc Do - This work may not be copied for use in Academic Coursework.
```

# Overview of the Snake Game functionality

The Snake Game program is currently capable of: 

    - Handles HTTP requests via Server class
    - Connects to a database to store and retrieve game and player data using a secure connection string.
    - Multiplayer Support: Allows multiple clients to connect and interact simultaneously.
    - Gameplay Mechanics: Players can control their snakes and compete in real-time.
    - Death Animation: Displays visually engaging animations when a snake dies.
    - Power-ups and Obstacles: Integrates power-ups and wall mechanics to enhance gameplay dynamics.
    

# Branching

- We share the main branch as the final result.
- Phuc Branch: Phuc focused on developing and refining the Database Service class, ensuring that updates to the game table and player table were handled correctly and efficiently.(Work completed by Bao Do)
- Khang Branch: Khang worked on the web server class, designing the HTML pages for displaying data, integrating the server class functionality, and implementing features to WebServer class. (Work completed by Khang Nguyen)

- PhucBranch commits: 
- KhangBranch commits: 297

- Each time the project is modified, the coder needs to commit and push to his local branch. Then he needs to merge to main and sync it to the main branch.
- To get the latest progress, the coder would need to merge from main to his own local branch and pull it. 

# Consulted Peers:

    - Khang Nguyen
    - Lan Huynh

# Time Expenditures:

        Hourse Estimated/Worked             Notes: The last assignment was challenging due to the limited time we had to familiarize ourselves with SQL and SQL Server Management Studio (SSMS). Adjusting to these tools while implementing the required functionality added complexity to the development process, making it a demanding task.
        17 / 20

- The estimation for the completion time is getting more accurate and relatively close to the actual time. This proves that the ability to complete the task on time is managable and the problem solving ability is improving.

- The partnership is most effective in brainstorming and deep understand of the lab's code, secondly, it is effective when dividing the work load. 
By collaborating early in the process, we were able to clarify complex sections and gain a stronger grasp on the codebase and the functionality of git branches. 
When it came to dividing the workload, assigning tasks based on each person’s strengths and areas of interest proved highly effective.

- As we progressed, we identified areas for improvement, particularly in communication during independent work. 
Early on, infrequent check-ins occasionally led to misaligned implementations, requiring additional debugging and integration effort. 
Over time, our task division became more balanced as we refined our process, ensuring tasks were appropriately matched to their complexity and to each team member’s skills, leading to more streamlined progress.

# Examples of Good Software Practice:
- DRY (Don't Repeat Yourself): Khang’s approach to frontend development emphasized reusability, such as when integrating images and designing player name display components. By structuring these elements for reuse, future enhancements to the game’s visuals and UI can be implemented more efficiently, reducing development time and maintaining consistency.
- Separation of Concerns: The division of responsibilities between Phuc and Khang exemplifies a strong separation of concerns. Phuc focused on backend gameplay features like zooming and drawing mechanics, while Khang handled the frontend, including UI design and player-related features. This separation ensures that each part of the codebase is modular, easier to maintain, and less prone to conflicts.
- Abstraction: Core gameplay and UI functionalities, such as zooming, drawing, and displaying player names, were abstracted into well-defined methods and classes. This abstraction simplifies testing, enables easier customization, and ensures the system can be extended or reused in future projects.