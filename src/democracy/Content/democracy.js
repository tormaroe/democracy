var democracy = {

    deleteItem: function(id) {
        if(confirm("Are you sure you want to permanently delete this item?")) {
            document.location = 'admin/delete/' + id;
        }
    }
};