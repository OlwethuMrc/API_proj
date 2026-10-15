CREATE DATABASE TheRaceDayDb;

USE TheRaceDayDb;


CREATE TABLE Users
(
    user_id INT IDENTITY(1,1) PRIMARY KEY,

    username VARCHAR(50) NOT NULL UNIQUE,

    password_hash VARCHAR(255) NOT NULL,

    role VARCHAR(20) NOT NULL,

    first_name VARCHAR(50) NOT NULL,

    last_name VARCHAR(50) NOT NULL,

    email VARCHAR(100) NOT NULL UNIQUE,

    phone VARCHAR(20) NOT NULL,

    created_at DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT CK_Users_Role
    CHECK (role IN ('Organiser', 'Participant'))
);


CREATE TABLE Organisers
(
    organiser_id INT IDENTITY(1,1) PRIMARY KEY,

    user_id INT NOT NULL UNIQUE,

    organisation_name VARCHAR(100) NOT NULL,

    created_at DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Organisers_Users
    FOREIGN KEY (user_id)
    REFERENCES Users(user_id)
);



CREATE TABLE Participants
(
    participant_id INT IDENTITY(1,1) PRIMARY KEY,

    user_id INT NOT NULL UNIQUE,

    date_of_birth DATE NOT NULL,

    gender VARCHAR(20) NOT NULL,

    created_at DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Participants_Users
    FOREIGN KEY (user_id)
    REFERENCES Users(user_id),

    CONSTRAINT CK_Participants_Gender
    CHECK (gender IN ('Male', 'Female', 'Other'))
);


CREATE TABLE Categories
(
    category_id INT IDENTITY(1,1) PRIMARY KEY,

    name VARCHAR(100) NOT NULL UNIQUE,

    description VARCHAR(255) NULL
);



CREATE TABLE Events
(
    event_id INT IDENTITY(1,1) PRIMARY KEY,

    organizer_id INT NOT NULL,

    category_id INT NOT NULL,

    name VARCHAR(100) NOT NULL,

    description VARCHAR(500) NULL,

    event_date DATE NOT NULL,

    location VARCHAR(150) NOT NULL,

    max_participants INT NOT NULL,

    created_at DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Events_Organisers
    FOREIGN KEY (organizer_id)
    REFERENCES Organisers(organiser_id),

    CONSTRAINT FK_Events_Categories
    FOREIGN KEY (category_id)
    REFERENCES Categories(category_id),

    CONSTRAINT CK_Events_MaxParticipants
    CHECK (max_participants > 0)
);


CREATE TABLE EventEnrollments
(
    enrollment_id INT IDENTITY(1,1) PRIMARY KEY,

    event_id INT NOT NULL,

    participant_id INT NOT NULL,

    enrolled_at DATETIME NOT NULL DEFAULT GETDATE(),

    status VARCHAR(20) NOT NULL DEFAULT 'Registered',

    CONSTRAINT FK_EventEnrollments_Events
    FOREIGN KEY (event_id)
    REFERENCES Events(event_id),

    CONSTRAINT FK_EventEnrollments_Participants
    FOREIGN KEY (participant_id)
    REFERENCES Participants(participant_id),

    CONSTRAINT UQ_EventEnrollments
    UNIQUE (event_id, participant_id),

    CONSTRAINT CK_EventEnrollments_Status
    CHECK (status IN ('Registered', 'Cancelled', 'Completed'))
);


CREATE TABLE Results
(
    result_id INT IDENTITY(1,1) PRIMARY KEY,

    event_id INT NOT NULL,

    participant_id INT NOT NULL,

    position INT NOT NULL,

    finish_time TIME NOT NULL,

    created_at DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Results_Events
    FOREIGN KEY (event_id)
    REFERENCES Events(event_id),

    CONSTRAINT FK_Results_Participants
    FOREIGN KEY (participant_id)
    REFERENCES Participants(participant_id),

    CONSTRAINT UQ_Results
    UNIQUE (event_id, participant_id),

    CONSTRAINT CK_Results_Position
    CHECK (position > 0)
);


INSERT INTO Users
(
    username,
    password_hash,
    role,
    first_name,
    last_name,
    email,
    phone
)
VALUES
(
    'raceorganizer1',
    'hashedpassword123',
    'Organizer',
    'John',
    'Smith',
    'john@capetownraces.co.za'
    
),
(
    'raceorganizer2',
    'hashedpassword456',
    'Organizer',
    'Sarah',
    'Mokoena',
    'sarah@joburgraces.co.za'
    
),
(
    'runner01',
    'hashedpassword789',
    'Participant',
    'Lebo',
    'Mkhize',
    'lebo@example.com'
    
),
(
    'runner02',
    'hashedpassword101',
    'Participant',
    'Thabo',
    'Dlamini',
    'thabo@example.com'
    
);

INSERT INTO Organisers
(
    user_id,
    organisation_name
)
VALUES
(
    1,
    'Cape Town Road Events'
),
(
    2,
    'Johannesburg Race Events'
);


INSERT INTO Participants
(
    user_id,
    date_of_birth,
    gender
)
VALUES
(
    3,
    '1999-05-15',
    'Female'
),
(
    4,
    '1997-08-20',
    'Male'
);


INSERT INTO Categories
(
    name,
    description
)
VALUES
(
    '5KM Run',
    'A five kilometre road running event.'
),
(
    '10KM Run',
    'A ten kilometre road running event.'
),
(
    'Half Marathon',
    'A 21.1 kilometre road running event.'
),
(
    'Cycling',
    'A competitive cycling event.'
);


INSERT INTO Events
(
    organizer_id,
    category_id,
    name,
    description,
    event_date,
    location,
    max_participants
)
VALUES
(
    1,
    1,
    'Cape Town Community 5K',
    'A community road running event for participants of all levels.',
    '2026-11-10',
    'Cape Town',
    300
),
(
    1,
    2,
    'Cape Town 10K Challenge',
    'A competitive ten kilometre road running event.',
    '2026-11-15',
    'Cape Town',
    500
),
(
    2,
    3,
    'Johannesburg Half Marathon',
    'A 21.1 kilometre road race.',
    '2026-12-01',
    'Johannesburg',
    1000
);


INSERT INTO EventEnrollments
(
    event_id,
    participant_id,
    status
)
VALUES
(
    1,
    1,
    'Registered'
),
(
    1,
    2,
    'Registered'
),
(
    2,
    1,
    'Registered'
),
(
    3,
    2,
    'Registered'
);


INSERT INTO Results
(
    event_id,
    participant_id,
    position,
    finish_time
)
VALUES
(
    1,
    1,
    1,
    '00:22:35'
),
(
    1,
    2,
    2,
    '00:24:10'
);


SELECT * FROM Users;

SELECT * FROM Organisers;

SELECT * FROM Participants;

SELECT * FROM Categories;

SELECT * FROM Events;

SELECT * FROM EventEnrollments;

SELECT * FROM Results;