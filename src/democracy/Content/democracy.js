var democracy = {};

(function () {
    var get = function (url) { document.location = url; };

    democracy.deleteItem = function(id) {
        if(confirm("Are you sure you want to permanently delete this item?")) {
            get('admin/delete/' + id);
        }
    };

    democracy.generateToken = function () { get('admin/generate-token'); };

    democracy.voteUp = function(id) { get('vote-up/' + id); };
    democracy.voteDown = function (id) { get('vote-down/' + id); };

})();