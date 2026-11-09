CREATE DATABASE TheRaceDayDb;

USE TheRaceDayDb;


CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role VARCHAR(20) NOT NULL CHECK (role IN ('Organiser', 'Participant')),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone VARCHAR(20) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Organisers (
    organiser_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Users(user_id),
    organisation_name VARCHAR(100) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Participants (
    participant_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Users(user_id),
    date_of_birth DATE NOT NULL,
    gender VARCHAR(20) NOT NULL CHECK (gender IN ('Male', 'Female', 'Other')),
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Category (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description VARCHAR(255) NULL
);

CREATE TABLE Events (
    event_id INT IDENTITY(1,1) PRIMARY KEY,
    organiser_id INT NOT NULL FOREIGN KEY REFERENCES Organisers(organiser_id),
    category_id INT NOT NULL FOREIGN KEY REFERENCES Category(category_id),
    name VARCHAR(100) NOT NULL,
    description VARCHAR(500) NULL,
    event_date DATE NOT NULL,
    location VARCHAR(150) NOT NULL,
    max_participants INT NOT NULL CHECK (max_participants > 0),
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Enrolment (
    enrolment_id INT IDENTITY(1,1) PRIMARY KEY,
    event_id INT NOT NULL FOREIGN KEY REFERENCES Events(event_id),
    participant_id INT NOT NULL FOREIGN KEY REFERENCES Participants(participant_id),
    enroled_at DATETIME NOT NULL DEFAULT GETDATE(),
    status VARCHAR(20) NOT NULL DEFAULT 'Registered' CHECK (status IN ('Registered', 'Cancelled', 'Completed')),
    CONSTRAINT UQ_Enrolment UNIQUE (event_id, participant_id)
);

CREATE TABLE Result (
    result_id INT IDENTITY(1,1) PRIMARY KEY,
    event_id INT NOT NULL FOREIGN KEY REFERENCES Events(event_id),
    participant_id INT NOT NULL FOREIGN KEY REFERENCES Participants(participant_id),
    position INT NOT NULL CHECK (position > 0),
    finish_time TIME NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_Result UNIQUE (event_id, participant_id)
);

-- Insert Data---
INSERT INTO Users (username, password_hash, role, first_name, last_name, email, phone) VALUES
('raceorganiser1', 'JSpassword123', 'Organiser', 'John', 'Smith', 'john@capetownraces.co.za', '0821112223'),
('raceorganiser2', 'SMpassword456', 'Organiser', 'Sarah', 'Mokoena', 'sarah@joburgraces.co.za', '0834445556'),
('runner01', 'LeboM789', 'Participant', 'Lebo', 'Mkhize', 'lebo@gmail.com', '0712345678'),
('runner02', 'ThaboD101', 'Participant', 'Thabo', 'Dlamini', 'thabo@gmail.com', '0723456789'),
('runner03', 'ChloeVDM102', 'Participant', 'Chloe', 'Van Der Merwe', 'chloe@gmail.com', '0734567890'),
('runner04', 'SiphoZ103', 'Participant', 'Sipho', 'Zulu', 'sipho@gmail.com', '0745678901'),
('runner05', 'JessicaN104', 'Participant', 'Jessica', 'Naidoo', 'jessica@gmail.com', '0756789012');

INSERT INTO Organisers (user_id, organisation_name) VALUES (1, 'Cape Town Road Events'), (2, 'Johannesburg Race Events');

INSERT INTO Participants (user_id, date_of_birth, gender) VALUES 
(3, '1999-05-15', 'Female'), (4, '1997-08-20', 'Male'), 
(5, '2001-02-10', 'Female'), (6, '1995-11-30', 'Male'), (7, '1998-07-22', 'Female');

INSERT INTO Category (name, description) VALUES 
('5KM Run', 'A five kilometre road running event.'),
('10KM Run', 'A ten kilometre road running event.'),
('Half Marathon', 'A 21.1 kilometre road running event.'),
('Cycling', 'A competitive cycling event.');

INSERT INTO Events (organiser_id, category_id, name, description, event_date, location, max_participants) VALUES 
(1, 1, 'Cape Town Community 5K', 'A community road running event for participants of all levels.', '2026-11-10', 'Cape Town', 300),
(1, 2, 'Cape Town 10K Challenge', 'A competitive ten kilometre road running event.', '2026-11-15', 'Cape Town', 500),
(2, 3, 'Johannesburg Half Marathon', 'A 21.1 kilometre road race.', '2026-12-01', 'Johannesburg', 1000);

INSERT INTO Enrolment (event_id, participant_id, status) VALUES 
(1, 1, 'Registered'), (1, 2, 'Registered'), (1, 3, 'Registered'), (1, 4, 'Registered'),
(2, 1, 'Registered'), (2, 5, 'Registered'), (3, 2, 'Registered'), (3, 3, 'Registered');

INSERT INTO Result (event_id, participant_id, position, finish_time) VALUES 
(1, 1, 1, '00:22:35'), (1, 2, 2, '00:24:10'), (1, 3, 3, '00:25:50');

-- Query all tables to view current state
SELECT * FROM Users; SELECT * FROM Organisers; SELECT * FROM Participants; 
SELECT * FROM Category; SELECT * FROM Events; SELECT * FROM Enrolment; SELECT * FROM Result;