function setPasswordModal() {
    const modal = new bootstrap.Modal(document.getElementById('password-modal'));
    document.getElementById('password-modal-button').onclick = function () {
        saveNewPassword();
    }
    modal.show();
}

async function saveNewPassword() {
    const form = document.getElementById('password-modal-body').querySelector('form');
    const id = form.querySelector('#Id').value;
    const pass = form.querySelector('#NewPassword').value;
    const rawResponse = await fetch('/admin/user/password', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "id": id,
            "newPassword": pass
        })
    });
    if (rawResponse.ok) {
        alert('Password successfully changed.');
    } else {
        alert('Sorry... Password change wasn\'t completed.')
    }
    closePasswordModal();

}

function closePasswordModal() {
//    document.getElementById("backdrop").style.display = "none"
    document.getElementById('passwordModal').style.display = "none"
    document.getElementById('passwordModal').classList.remove("show")
}






//    const rawResponse = await fetch("/api/trainer/" + id, {
//        method: 'POST'
//    });
//    if (rawResponse.ok) {
//        const trainer = await rawResponse.json();
//        if (trainer != null && trainer.status != 404) {
//            document.getElementById("trainer-modal-fullname").innerText = trainer.firstName + " " + trainer.lastName;
//            document.getElementById("trainer-modal-description").innerHTML = trainer.description;
//            document.getElementById("trainer-modal-department").innerText = trainer.department;
//            document.getElementById("trainer-modal-photo").src = "data:image/png;base64," + trainer.photo;
//            const modal = new bootstrap.Modal(document.getElementById('trainer-modal-window'));
//            modal.show();
//        }
//    }
//};