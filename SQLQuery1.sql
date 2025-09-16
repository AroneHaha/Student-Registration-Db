CREATE TABLE Students (
    StudentId INT NOT NULL PRIMARY KEY,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    MiddleInitial VARCHAR(5) NOT NULL,
    Program VARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL,
    Age INT NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Address VARCHAR(100) NOT NULL,
    ContactNum VARCHAR(15) NOT NULL
);

INSERT INTO Students (StudentId, FirstName, LastName, MiddleInitial, Program, BirthDate, Age, Gender, Address, ContactNum)
VALUES
(2000, 'Lelouch', 'Lamperouge', 'V', 'BS Information Technology', '2001-12-05', 23, 'Male', 'Ashford Academy Dorms', '09170002000'),
(2001, 'Johan', 'Liebert', 'K', 'BS Psychology', '2000-04-12', 25, 'Male', 'Munich, Germany', '09170002001'),
(2002, 'Rintaro', 'Okabe', 'H', 'BS Computer Science', '2002-01-14', 23, 'Male', 'Akihabara, Tokyo', '09170002002'),
(2003, 'Alphonse', 'Elric', 'E', 'BS Mechanical Engineering', '2003-05-19', 22, 'Male', 'Resembool, Amestris', '09170002003'),
(2004, 'Subaru', 'Natsuki', 'K', 'BS Nursing', '2001-03-30', 24, 'Male', 'Lugnica Kingdom', '09170002004');
