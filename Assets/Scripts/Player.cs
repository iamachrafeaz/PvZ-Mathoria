using System;


public class Player
{
    string uid;
    public string firstName;
    string lastName;
    int schoolGrade;

    public Player(string uid, string firstName, string lastName, int schoolGrade)
    {
        this.uid = uid;
        this.firstName = firstName;
        this.lastName = lastName;
        this.schoolGrade = schoolGrade;
    }

    public string getUid()
    {
        return uid;
    }

    public string getFirstName()
    {
        return firstName;
    }

    public string getLastName()
    {
        return lastName;
    }

    public int getSchoolGrade()
    {
        return schoolGrade;
    }

    public string getFullName()
    {
        return firstName + " " + lastName;
    }

}
