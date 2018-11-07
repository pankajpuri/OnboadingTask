/// <reference path="bootstrap.min.js" />
//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
    (function ($) {
        var contactInfo = {
            Name: ko.observable($("#Name").val()).extend({
                minLength: {
                    params: 2,
                    message: "Invalid Name , Please Enter Atleast 2 Letters"
                }, maxLength: 50
            }),
            Address: ko.observable($("#Address").val()).extend({
                minLength: {
                    params: 2,
                    message: "Invalid Address , Please Enter Atleast 2 Letters"
                }, maxLength: 200
            })
        };


        ko.applyBindings(contactInfo);
    }($))
   

    
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/Customers/GetCustomer",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Address + '</td>';
                html += '<td><a href="#" class="btn btn-warning" onclick="return getbyID(' + item.Id + ')">Edit</a></td>';
                html += '<td> <a href="#"  class="btn btn-danger" onclick="Delele(' + item.Id + ')">Delete</a></td > ';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
}

//Add Data Function
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var cusOjb = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        Address: $('#Address').val()
    };
    $.ajax({
        url: "/Customers/Add",
        data: JSON.stringify(cusOjb),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for getting the Data Based upon Employee ID
function getbyID(Id) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Customers/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#Address').val(result.Address);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;




}
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var empObj = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        Address: $('#Address').val(),
    };
    $.ajax({
        url: "/Customers/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Name').val("");
            $('#Address').val("");
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
}



//Function for clearing the textboxes
function clearTextBox() {
   
    $('#Name').val("");
    $('#Address').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
}


//function for deleting employee's record
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Customers/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');

        isValid = false;

    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        isValid = false;

    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }

    return isValid;
}