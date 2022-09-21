﻿function onNameChange() // when a teacher or staff's name is entered
{
    var firstName = $("#Staff_FirstName").val(); // stores the value of the staff's first name in a variable
    var lastName = $("#Staff_LastName").val(); // stores the value of the staff's last name in a variable

    if (firstName !== "" && lastName !== "") // when the entered staff name is not null
    {
        var TeacherCode = $("#Staff_TeacherCode").val(firstName.substring(0, 1).toUpperCase() + lastName.substring(0, 2).toUpperCase());
        // sets the value of the teacher's code in a variable by taking the first character of the staff's first name and makes them upper case, and adds the first 2 characters of the staff's last name and makes them uppercase
    }
}