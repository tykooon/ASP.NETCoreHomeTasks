async function confirmDelete(id, fullName) {
    if (!id) {
        return;
    }

    let answer = confirm('Do you really want delete ' + fullName + ' ?');
    if (!answer) {
        return;
    }

    var rawResponse = await fetch('/admin/user/?id='+id, {
        method: 'DELETE'
    });

    if (!rawResponse.ok) {
        alert("Sorry... Delete operation cannot be performed.");
        return;
    }

    window.location = "/user";
}