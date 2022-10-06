const BASE_URL = "/api";

function loadUsers() {
    $.ajax({
        url: BASE_URL + "/users",
        type: "get",
        success: function (users) {
            $("#usersTable tbody").empty();
            $("#userSelect").empty();

            for (user of users) {
                $("#usersTable tbody").append(
                    '<tr class="table-dark"><th scope="row">' + user.id + '</th><td>' + user.first_name + '</td><td>' + user.last_name + '</td></tr>'
                );

                $("#userSelect").append(
                    '<option value="' + user.id + '">' + user.first_name + ' ' + user.last_name + '</option>'
                );
            }
        }
    })
};

function loadRoles() {
    $.ajax({
        url: BASE_URL + "/roles",
        type: "get",
        success: function (roles) {
            $("#roleTable tbody").empty();
            $("#roleSelect").empty();

            for (role of roles) {
                $("#roleTable tbody").append(
                    '<tr class="table-primary"><th scope="row">' + role.id + '</th><td>' + role.name + '</td></tr>'
                );

                $("#roleSelect").append(
                    '<option value="' + role.id + '">' + role.name + '</option>'
                );
            }
        }

    })
};

function loadUserRoles() {
    $.ajax({
        url: BASE_URL + "/users/roles",
        type: "get",
        success: function (userroles) {
            $("#userRolesList").empty();

            for (ur of userroles) {
                for (r of ur.roles) {
                    $("#userRolesList").append(
                        '<li class="list-group-item">Role <b> ' + r.name + '</b> has been assigned to user <b>' + ur.first_name + '</b></li>'
                    )
                }
            }
        }
    })
};

$(document).ready(function () {
    loadUsers();
    loadRoles();
    loadUserRoles();

    $("#createUser").on('click', (event) => {
        const user = {
            first_name: "",
            last_name: ""
        }
        user.first_name = $("#firstName").val();
        user.last_name = $("#lastName").val();

        $.ajax({
            url: BASE_URL + "/users",
            type: "post",
            data: JSON.stringify(user),
            contentType: "application/json",
            dataType: "json",
            statusCode: {
                201: function (rsp) {
                    loadUsers();
                }
            }
        });
    });

    $("#createRole").on('click', (event) => {
        const role = {
            name: ""
        }
        role.name = $("#roleName").val();


        $.ajax({
            url: BASE_URL + "/roles",
            type: "post",
            data: JSON.stringify(role),
            contentType: "application/json",
            dataType: "json",
            statusCode: {
                201: function (rsp) {
                    loadRoles();
                }
            }
        });
    });

    $("#assignRole").on('click', (event) => {
        const userId = $("#userSelect").val();
        const roleId = $("#roleSelect").val();


        $.ajax({
            url: BASE_URL + "/users/" + userId + "/roles/" + roleId,
            type: "put",
            success: function (rsp) {
                loadUserRoles();
            }
        });
    });

});