# Chat API Project: Real-Time Messaging

-----

## Project Description

The **"Chat API"** project aims to develop a comprehensive **real-time messaging API** that enables users to communicate instantly. This API will support essential features such as **user registration**, **friend management**, **one-on-one messaging**, and **group chats**. It will serve as the foundational backend for various chat applications, ensuring seamless and interactive communication experiences.

In today's digital age, real-time communication is crucial for both personal and professional interactions. This project focuses on building a robust API that handles all necessary functionalities for a modern chat application.

## Key Features & User Interactions

Here's a detailed look at how users will interact with the application powered by this API:

### User Registration and Authentication

  * **Sign Up**: New users can easily create an account by providing a unique **username**, **email**, and **password**. Account verification will be done via a confirmation email.
  * **Login**: Registered users can securely log in using their **email** and **password**. For enhanced security, **multi-factor authentication (MFA)** will be supported.

### Friend Management

  * **Add Friends**: Users can send **friend requests** to others. Once accepted, they can view each other’s **online status** and initiate chats.
  * **Remove Friends**: Users have the flexibility to remove friends from their list, which will also conclude any ongoing conversations with them.

### One-on-One Messaging

  * **Send and Receive Messages**: Users can exchange **text messages, images, and files** with their friends in real-time. The API will support **delivery receipts** and **read receipts** for reliable communication.
  * **View Chat History**: Users can access and review their entire conversation history, including all sent and received messages.

### Group Chats

  * **Create Group Chats**: Users can easily create group conversations by adding multiple friends. They can **name the group** and set a **group picture**.
  * **Manage Group Chats**: Group creators and administrators can **add or remove members**, **change the group name**, and **update the group picture**. All members will instantly see these updates.
  * **Send and Receive Messages in Groups**: Members can communicate effectively within group chats, with features like **replying to specific messages** and **mentioning other users**.

### Real-Time Notifications

  * **Message Notifications**: Users will receive instant **notifications for new messages**, applicable to both one-on-one and group chats.
  * **Friend Request Notifications**: Users will be notified of **incoming friend requests** and when their **sent requests are accepted**.

-----

## Real-World Example

Imagine **Emma**, who relies on a chat application to stay connected with her friends and coordinate group projects.

Emma begins by signing up for an account using her email and a secure password. After logging in, she builds her contact list by sending **friend requests** to her peers.

She initiates a **one-on-one conversation** with her friend John, sharing updates and exchanging images. Thanks to the real-time messaging capabilities, their messages appear instantly for both of them. Emma also creates a **group chat** for her study group, adding multiple friends. This group becomes their central hub for sharing resources, discussing assignments, and planning meetings.

Crucially, whenever Emma receives a new message or a friend request, she gets a **real-time notification**, ensuring she remains fully updated on all interactions.

-----

## Project Specifications

### 1\. Introduction

The "Chat API" project aims to develop a comprehensive **real-time messaging API** that supports user registration, friend management, one-on-one messaging, and group chats. Users will be able to communicate instantly and manage their contacts and conversations effectively.

### 2\. Objectives

  * Allow users to **sign up, log in, and manage their accounts**.
  * Enable users to **add and remove friends**.
  * Support **real-time one-on-one messaging**.
  * Provide features for **creating and managing group chats**.
  * Ensure users receive **real-time notifications** for messages and friend requests.

### 3\. Functional Requirements

#### User Management

  * **Sign Up**: Users can create an account by providing a username, email, and password.
  * **Login**: Users can log in using their email and password.
  * **Profile Management**: Users can update their profile information.

#### Friend Management

  * **Add Friends**: Users can send friend requests to other users.
  * **Remove Friends**: Users can remove friends from their list.

#### Messaging

  * **Send and Receive Messages**: Users can send and receive text messages, images, and files.
  * **View Chat History**: Users can view their chat history.

#### Group Chats

  * **Create Group Chats**: Users can create group chats and add friends.
  * **Manage Group Chats**: Users can add or remove members and update group details.
  * **Send and Receive Messages in Groups**: Users can communicate within group chats.

#### Notifications

  * **Message Notifications**: Users receive notifications for new messages.
  * **Friend Request Notifications**: Users receive notifications for friend requests.

### 4\. Non-Functional Requirements

  * **Scalability**: The API should handle a growing number of users and messages efficiently.
  * **Performance**: The API should have a fast response time and handle concurrent requests efficiently.
  * **Security**: Implement robust authentication and authorization mechanisms to protect user data.
  * **Reliability**: The API should be highly available and handle failures gracefully.
  * **Usability**: The API should be easy to use and well-documented for developers.

### 5\. Use Cases

  * **User Sign Up and Login**: New users sign up and existing users log in.
  * **Friend Management**: Users add and remove friends.
  * **One-on-One Messaging**: Users send and receive messages in private chats.
  * **Group Chat Management**: Users create and manage group chats.
  * **Receive Notifications**: Users receive notifications for new messages and friend requests.

### 6\. User Stories

  * As a user, I want to **sign up** for an account so that I can chat with others.
  * As a user, I want to **log in** to my account so that I can access my chats.
  * As a user, I want to **add friends** so that I can chat with them.
  * As a user, I want to **remove friends** so that I can manage my contacts.
  * As a user, I want to **send messages** so that I can communicate in real-time.
  * As a user, I want to **receive messages instantly** so that I can stay updated.
  * As a user, I want to **create group chats** so that I can chat with multiple friends.
  * As a user, I want to **manage group chats** so that I can add or remove members.
  * As a user, I want to **receive notifications for new messages** so that I stay informed.
  * As a user, I want to **receive notifications for friend requests** so that I can manage my contacts.

### 7\. Technical Requirements

  * **Programming Language**: Choose an appropriate backend language (e.g., Node.js, Python, Ruby).
  * **Database**: Use a database to store user, message, and interaction data (e.g., PostgreSQL, MongoDB).
  * **Authentication**: Implement **JWT (JSON Web Token)** for secure user authentication.
  * **Real-Time Communication**: Utilize **WebSockets** or a similar technology for real-time messaging.
  * **API Documentation**: Use **Swagger** or similar tools for comprehensive API documentation.

### 8\. API Endpoints (Optimized Design)

#### User Management

  * **`POST /users`**: Register a new user.
      * **Reasoning**: More RESTful to create a resource (`user`) under its collection (`/users`). Previously `/signup`.
      * **Request Body Example:**
        ```json
        {
          "username": "newuser",
          "email": "newuser@example.com",
          "password": "securepassword123"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "User registered successfully. Please check your email for verification.",
          "userId": "uuid-1234-abcd"
        }
        ```
  * **`POST /auth/login`**: Authenticate a user.
      * **Reasoning**: Grouping authentication-related actions under `/auth` is a common pattern. Previously `/login`.
      * **Request Body Example:**
        ```json
        {
          "email": "user@example.com",
          "password": "securepassword123"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Login successful",
          "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
          "userId": "uuid-abcd-5678"
        }
        ```
  * **`GET /users/me`**: Get current authenticated user's profile details.
      * **Reasoning**: Explicitly refers to the authenticated user's profile, consistent with other resources being `/users/{id}`. Previously `/profile`.
      * **Successful Response Example:**
        ```json
        {
          "userId": "uuid-abcd-5678",
          "username": "CurrentUserName",
          "email": "user@example.com",
          "status": "online",
          "profilePictureUrl": "http://example.com/profile/currentuser.jpg"
        }
        ```
  * **`PATCH /users/me`**: Update current authenticated user's profile.
      * **Reasoning**: `PATCH` is preferred over `PUT` for partial updates, which is typical for profile changes (e.g., changing only username, not email/password). If the entire resource *must* be replaced, `PUT` is fine.
      * **Request Body Example:**
        ```json
        {
          "username": "UpdatedUserName",
          "profilePictureUrl": "http://example.com/profile/updateduser.jpg"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Profile updated successfully",
          "userId": "uuid-abcd-5678"
        }
        ```

#### Friend Management

  * **`POST /friend-requests`**: Send a friend request.
      * **Reasoning**: Treats friend requests as a distinct resource. This allows for clear "create," "accept," and "reject" actions. Previously `/friends/add`.
      * **Request Body Example:**
        ```json
        {
          "recipientUserId": "uuid-efgh-9012"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Friend request sent successfully",
          "requestId": "req-1234"
        }
        ```
  * **`PATCH /friend-requests/{requestId}`**: Accept or reject a friend request.
      * **Reasoning**: Using `PATCH` on the `friend-request` resource to update its status (e.g., to 'accepted' or 'rejected').
      * **Request Body Example (Accept):**
        ```json
        {
          "status": "accepted"
        }
        ```
      * **Request Body Example (Reject):**
        ```json
        {
          "status": "rejected"
        }
        ```
      * **Successful Response Example (Accept):**
        ```json
        {
          "message": "Friend request accepted",
          "friendshipId": "fs-5678"
        }
        ```
      * **Successful Response Example (Reject):**
        ```json
        {
          "message": "Friend request rejected"
        }
        ```
  * **`GET /users/me/friends`**: List current user's friends.
      * **Reasoning**: A common pattern for listing associated resources.
      * **Successful Response Example:**
        ```json
        [
          {
            "userId": "uuid-efgh-9012",
            "username": "JohnDoe",
            "status": "online",
            "lastSeen": "2024-07-12T09:00:00Z"
          },
          {
            "userId": "uuid-ijkl-3456",
            "username": "JaneSmith",
            "status": "offline",
            "lastSeen": "2024-07-11T18:00:00Z"
          }
        ]
        ```
  * **`DELETE /users/me/friends/{friendId}`**: Remove a friend.
      * **Reasoning**: Standard RESTful way to delete a specific resource. Previously `/friends/remove`.
      * **Path Parameter**: `friendId` (ID of the friend to remove)
      * **Successful Response Example:**
        ```json
        {
          "message": "Friend removed successfully"
        }
        ```

#### Messaging & Conversations

  * **`POST /messages`**: Send a new message.
      * **Description**: User A sends a message (to a specific user or group). The server responds with confirmation, implying the "single tick" status for User A.
      * **Request Body Example (One-on-One):**
        ```json
        {
          "recipientType": "user",
          "recipientId": "uuid-efgh-9012",
          "contentType": "text",
          "content": "Hello John, how are you?"
        }
        ```
      * **Request Body Example (Group):**
        ```json
        {
          "recipientType": "group",
          "recipientId": "grp-5678",
          "contentType": "text",
          "content": "Meeting at 3 PM, don't forget!"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Message sent to server successfully",
          "messageId": "msg-001",
          "conversationId": "conv-1234-5678", // Useful for client to identify chat
          "timestamp": "2024-07-12T10:00:00Z"
        }
        ```
  * **`GET /conversations`**: Retrieve all conversations (one-on-one and group).
      * **Reasoning**: Introduces a `conversation` resource that can represent both private chats and group chats, simplifying message retrieval.
      * **Successful Response Example:**
        ```json
        [
          {
            "conversationId": "conv-1234-5678",
            "type": "one-on-one",
            "participants": ["uuid-abcd-5678", "uuid-efgh-9012"],
            "lastMessage": {
              "messageId": "msg-001",
              "senderId": "uuid-abcd-5678",
              "contentType": "text",
              "content": "Hello John, how are you?",
              "timestamp": "2024-07-12T10:00:00Z"
            },
            "unreadCount": 0
          },
          {
            "conversationId": "grp-5678", // Group ID can serve as conversation ID
            "type": "group",
            "name": "Study Group Beta",
            "participants": ["uuid-abcd-5678", "uuid-efgh-9012", "uuid-ijkl-3456"],
            "lastMessage": { /* ... */ },
            "unreadCount": 5
          }
        ]
        ```
  * **`GET /conversations/{conversationId}/messages`**: Retrieve messages within a specific conversation.
      * **Reasoning**: Consistent with the new `conversations` resource. Replaces `GET /messages/{user_id}` and `GET /messages/group/{group_id}`.
      * **Path Parameter**: `conversationId`
      * **Query Parameters (for pagination):** `?limit=50&before=2024-07-12T09:00:00Z` (to get messages before a certain timestamp)
      * **Successful Response Example:**
        ```json
        [
          {
            "messageId": "msg-001",
            "conversationId": "conv-1234-5678",
            "senderId": "uuid-abcd-5678",
            "contentType": "text",
            "content": "Hello John, how are you?",
            "timestamp": "2024-07-12T10:00:00Z",
            "status": "read" // 'sent', 'delivered', 'read'
          },
          {
            "messageId": "msg-002",
            "conversationId": "conv-1234-5678",
            "senderId": "uuid-efgh-9012",
            "contentType": "text",
            "content": "I'm good, thanks! How about you?",
            "timestamp": "2024-07-12T10:01:00Z",
            "status": "read"
          }
        ]
        ```
  * **`POST /messages/delivery-receipt`**: User's device reports message delivered.
      * **Description**: This endpoint is called by the recipient's device when a message is successfully received and processed locally. This action will trigger the "double tick" update for the sender.
      * **Request Body Example:**
        ```json
        {
          "messageId": "msg-001",
          "deliveredAt": "2024-07-12T10:05:00Z"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Delivery receipt acknowledged",
          "status": "success"
        }
        ```
  * **`POST /messages/read-receipt`**: User's device reports message read.
      * **Description**: This endpoint is called by the recipient's device when a message is actually viewed/read by the user. This action will trigger the "blue tick" update for the sender (if read receipts are enabled).
      * **Request Body Example:**
        ```json
        {
          "messageId": "msg-001",
          "readAt": "2024-07-12T10:10:00Z"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Read receipt acknowledged",
          "status": "success"
        }
        ```

#### Group Chats

  * **`POST /groups`**: Create a new group chat.
      * **Reasoning**: Already good.
      * **Request Body Example:**
        ```json
        {
          "name": "Study Group Alpha",
          "memberUserIds": ["uuid-efgh-9012", "uuid-ijkl-3456"],
          "groupPictureUrl": "http://example.com/groups/study_alpha.jpg"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Group chat created successfully",
          "groupId": "grp-5678",
          "conversationId": "grp-5678" // Group ID can also serve as conversation ID
        }
        ```
  * **`GET /groups/{groupId}`**: Get details of a specific group chat.
      * **Reasoning**: Common to have a GET endpoint for a specific resource.
      * **Path Parameter**: `groupId`
      * **Successful Response Example:**
        ```json
        {
          "groupId": "grp-5678",
          "name": "Study Group Beta",
          "groupPictureUrl": "http://example.com/groups/study_beta.jpg",
          "adminIds": ["uuid-abcd-5678"],
          "memberIds": ["uuid-abcd-5678", "uuid-efgh-9012", "uuid-ijkl-3456"]
        }
        ```
  * **`PATCH /groups/{groupId}`**: Update group chat details.
      * **Reasoning**: `PATCH` for partial updates to group name or picture.
      * **Path Parameter**: `groupId`
      * **Request Body Example:**
        ```json
        {
          "name": "Study Group Beta",
          "groupPictureUrl": "http://example.com/groups/study_beta.jpg"
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Group details updated successfully",
          "groupId": "grp-5678"
        }
        ```
  * **`POST /groups/{groupId}/members`**: Add members to a group chat.
      * **Reasoning**: Treat `members` as a sub-resource. `POST` to a collection to add to it. Replaces `/groups/{group_id}/add`.
      * **Path Parameter**: `groupId`
      * **Request Body Example:**
        ```json
        {
          "memberUserIds": ["uuid-mnop-7890", "uuid-qrst-1234"]
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Members added to group successfully",
          "groupId": "grp-5678"
        }
        ```
  * **`DELETE /groups/{groupId}/members/{memberId}`**: Remove a member from a group chat.
      * **Reasoning**: Standard RESTful way to delete a specific resource from a sub-collection. Replaces `/groups/{group_id}/remove`.
      * **Path Parameters**: `groupId`, `memberId`
      * **Successful Response Example:**
        ```json
        {
          "message": "Member removed from group successfully",
          "groupId": "grp-5678"
        }
        ```
  * **`GET /groups/{groupId}/members`**: List members of a group chat.
      * **Reasoning**: Common to retrieve members of a group.
      * **Path Parameter**: `groupId`
      * **Successful Response Example:**
        ```json
        [
          {
            "userId": "uuid-abcd-5678",
            "username": "CurrentUserName",
            "role": "admin"
          },
          {
            "userId": "uuid-efgh-9012",
            "username": "JohnDoe",
            "role": "member"
          }
        ]
        ```

#### Notifications

  * **`GET /notifications`**: Retrieve the user's notifications.
      * **Query Parameters (for filtering/pagination):** `?status=unread&limit=20&offset=0`
      * **Successful Response Example:**
        ```json
        [
          {
            "notificationId": "notif-001",
            "type": "message",
            "sourceId": "uuid-efgh-9012",
            "messagePreview": "New message from John: 'Are you free for a call?'",
            "timestamp": "2024-07-12T12:00:00Z",
            "read": false
          },
          {
            "notificationId": "notif-002",
            "type": "friend_request",
            "sourceId": "uuid-qrst-1234",
            "message": "New friend request from Sarah.",
            "timestamp": "2024-07-12T09:30:00Z",
            "read": false
          }
        ]
        ```
  * **`PATCH /notifications/{notificationId}`**: Mark a specific notification as read.
      * **Reasoning**: `PATCH` for partial update (changing `read` status).
      * **Path Parameter**: `notificationId`
      * **Request Body Example:**
        ```json
        {
          "read": true
        }
        ```
      * **Successful Response Example:**
        ```json
        {
          "message": "Notification updated successfully"
        }
        ```
  * **`POST /notifications/mark-all-read`**: Mark all unread notifications as read.
      * **Reasoning**: An action endpoint for a bulk operation.
      * **Successful Response Example:**
        ```json
        {
          "message": "All unread notifications marked as read",
          "count": 5
        }
        ```
  * **`DELETE /notifications/{notificationId}`**: Delete a specific notification.
      * **Reasoning**: Standard RESTful way to delete a resource.
      * **Path Parameter**: `notificationId`
      * **Successful Response Example:**
        ```json
        {
          "message": "Notification deleted successfully"
        }
        ```

### 9\. Security

  * Use **HTTPS** to encrypt data in transit.
  * Implement **input validation and sanitization** to prevent SQL injection and XSS attacks.
  * Use **strong password hashing algorithms** like bcrypt.

### 10\. Performance

  * Implement **caching strategies** to improve response times.
  * **Optimize database queries** to handle large datasets efficiently.
  * Use **load balancing** to distribute traffic evenly across servers.

### 11\. Documentation

  * Provide comprehensive **API documentation** using tools like Swagger.
  * Create **user guides and developer documentation** to assist with integration and usage.

### 12\. Glossary

  * **API**: Application Programming Interface
  * **JWT**: JSON Web Token
  * **CRUD**: Create, Read, Update, Delete
  * **MFA**: Multi-Factor Authentication

### 13\. Appendix

  * Include any relevant diagrams, data models, and additional references.

-----