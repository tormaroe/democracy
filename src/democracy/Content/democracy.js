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
          , p2 = $("#password2").val();

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

})();