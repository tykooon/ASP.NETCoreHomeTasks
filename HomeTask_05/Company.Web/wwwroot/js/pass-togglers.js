function togglePass(passId) {
    let passInput = document.getElementById(passId);
    let eyeShow = document.getElementById(passId+'-eye-show');
    let eyeHide = document.getElementById(passId+'-eye-hide');
    switch (passInput.type) {
        case "text":
            passInput.type = "password";
            eyeShow.hidden = false;
            eyeHide.hidden = true;
            break;
        case "password":
            passInput.type = "text";
            eyeShow.hidden = true;
            eyeHide.hidden = false;
            break;
    };
}

