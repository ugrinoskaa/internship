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
                    '<tr class="table-dark"><th scope="row">' + user.id + '</th><td>' + user.first_name + '</td><td>' + user.last_name + '</td><td><button onclick="updateUser(' + user.id + ')" class="btn btn-outline-secondary">Edit</button> <button onclick="deleteUser(' + user.id + ')" class="btn btn-outline-danger">Delete</button></td></tr>'
                );

                $("#userSelect").append(
                    '<option value="' + user.id + '">' + user.first_name + ' ' + user.last_name + '</option>'
                );
            }
        }
    })
};

function loadUsersByName(name) {
    $.ajax({
        url: BASE_URL + "/users?name=" + name,
        type: "get",
        success: function (users) {
            $("#usersTable tbody").empty();

            for (user of users) {
                $("#usersTable tbody").append(
                    '<tr class="table-dark"><th scope="row">' + user.id + '</th><td>' + user.first_name + '</td><td>' + user.last_name + '</td><td><button onclick="updateUser(' + user.id + ')" class="btn btn-outline-secondary">Edit</button> <button onclick="deleteUser(' + user.id + ')" class="btn btn-outline-danger">Delete</button></td></tr>'
                );
            }
        }
    })
};

function loadRolesByName(name) {
    $.ajax({
        url: BASE_URL + "/roles?name=" + name,
        type: "get",
        success: function (roles) {
            $("#rolesTable tbody").empty();

            for (role of roles) {
                $("#rolesTable tbody").append(
                    '<tr class="table-primary"><th scope="row">' + role.id + '</th><td>' + role.name + '</td><td><button onclick="updateRole(' + role.id + ')" class="btn btn-outline-secondary">Edit</button> <button onclick="deleteRole(' + role.id + ')" class="btn btn-outline-danger">Delete</button></td></tr>'
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
            $("#rolesTable tbody").empty();
            $("#roleSelect").empty();

            for (role of roles) {
                $("#rolesTable tbody").append(
                    '<tr class="table-primary"><th scope="row">' + role.id + '</th><td>' + role.name + '</td><td><button onclick="updateRole(' + role.id + ')" class="btn btn-outline-secondary">Edit</button> <button onclick="deleteRole(' + role.id + ')" class="btn btn-outline-danger">Delete</button></td></tr>'
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
        url: BASE_URL + "/users",
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

function deleteUser(id) {
    $.ajax({
        url: BASE_URL + "/users/" + id,
        type: "delete",
        success: function () {
            loadUsers();
            loadUserRoles();
        }
    })
};

function updateUser(id) {
    $.ajax({
        url: BASE_URL + "/users/" + id,
        type: "get",
        success: function (user) {
            $("#createUserLegend").text("Edit User");
            $("#userId").val(user.id); // set value on hidden input html
            $("#firstName").val(user.first_name);
            $("#lastName").val(user.last_name);
            $("#createUser").text("Edit User");
        }
    })
};

function deleteRole(id) {
    $.ajax({
        url: BASE_URL + "/roles/" + id,
        type: "delete",
        success: function () {
            loadRoles();
            loadUserRoles();
        }
    })
};

function updateRole(id) {  // koga kje pritisnam Edit, da se stavi imeto vo field-ot i da se smenat Create Role => Edit Role
    $.ajax({
        url: BASE_URL + "/roles/" + id,
        type: "get",
        success: function (role) {
            $("#createRoleLegend").text("Edit Role");
            $("#roleName").val(role.name);
            $("#createRole").text("Edit Role");
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

        const userId = $("#userId").val();

        if (userId == "") {
            $.ajax({
                url: BASE_URL + "/users",
                type: "post",
                data: JSON.stringify(user),
                contentType: "application/json",
                dataType: "json",
                statusCode: {
                    201: function (rsp) {
                        loadUsers();
                        $("#firstName").val("");
                        $("#lastName").val("");
                        $("#createUserLegend").text("Create User");
                        $("#createUser").text("Create User");
                        $("#userId").val("");
                    }
                }
            });
        } else {
            $.ajax({
                url: BASE_URL + "/users/" + userId,
                type: "put",
                data: JSON.stringify(user),
                contentType: "application/json",
                dataType: "json",
                statusCode: {
                    200: function (rsp) {
                        loadUsers();
                        $("#firstName").val("");
                        $("#lastName").val("");
                        $("#createUserLegend").text("Create User");
                        $("#createUser").text("Create User");
                        $("#userId").val("");
                    }
                }
            });
        }
    });

    $("#createRole").on('click', (event) => {
        const role = {
            name: ""
        }
        role.name = $("#roleName").val();

        const roleId = $("#roleId").val();

        if (roleId == "") {
            $.ajax({
                url: BASE_URL + "/roles",
                type: "post",
                data: JSON.stringify(role),
                contentType: "application/json",
                dataType: "json",
                statusCode: {
                    201: function (rsp) {
                        loadRoles();
                        $("#roleName").val("");
                        $("#roleId").val("");
                        $("#createRoleLegend").text("Create Role");
                        $("#createRole").text("Create Role");
                    }
                }
            });
        } else {
            $.ajax({
                url: BASE_URL + "/roles/" + roleId,
                type: "put",
                data: JSON.stringify(role),
                contentType: "application/json",
                dataType: "json",
                statusCode: {
                    200: function (rsp) {
                        loadRoles();
                        $("#roleName").val("");
                        $("#roleId").val("");
                        $("#createRoleLegend").text("Create Role");
                        $("#createRole").text("Create Role");
                    }
                }
            });
        }
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

    $("#searchUserByName").on('keyup', (event) => {
        const value = event.currentTarget.value;

        if (value === "") {
            loadUsers();
        } else {
            loadUsersByName(value);
        }
    });

    $("#searchRoleByName").on('keyup', (event) => {
        const value = event.currentTarget.value;

        if (value == "") {
            loadRoles();
        } else {
            loadRolesByName(value);
        }
    });

});