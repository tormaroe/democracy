var democracy = {};

(function () {
    var get = function (url) { document.location = url; };

    democracy.deleteItem = function(id) {
        if(confirm("Are you sure you want to permanently delete this item?")) {
            get('admin/delete/' + id);
        }
    };

    democracy.generateToken = function () { get('admin/generate-token'); };

    democracy.voteUp = function (id) { get('vote-up/' + id); };
    democracy.voteDown = function (id) { get('vote-down/' + id); };
    democracy.activateUser = function (id) { get('activate-user'); };

    democracy.validateNewUser = function () {
        var p1 = $("#password1").val()
          , p2 = $("#password2").val()
          , username = $("#username").val()
          , token = $("#token").val();

        if (username === "" || username.length < 3) {
            alert("You obviously need to choose a username!\nIt needs to be at least 3 characters long.");
            return false;
        }
        if (token === "") {
            alert("Please provide the registration token you have been given.\nYou need this to get access to this site!");
            return false;
        }
        if (p1 !== p2) {
            alert("Password fields does not match! Try again!");
            return false;
        }
        if (p1 === "") {
            alert("You need a password! Try again!");
            return false;
        }
        return true;
    };

    democracy.validatePasswordChange = function () {
        var p1 = $("#password1").val()
          , p2 = $("#password2").val()
          , pOld = $("#oldPassword").val();

        if (pOld === "") {
            alert("Please provide the old password! Try again!");
            return false;
        }
        if (p1 !== p2) {
            alert("New password fields does not match! Try again!");
            return false;
        }
        if (p1 === "") {
            alert("You need to provide a new password! Try again!");
            return false;
        }
        return true;
    };

})();