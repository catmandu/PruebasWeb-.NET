/// <reference path="jquery-1.10.2.min.js" />

function getAccessToken(){
    if (location.hash) {
        if(location.hash.split('access_token=')){
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
}

function isUserRegistered(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type':'application/json',
            'Authorization':'Bearer '+accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                sessionStorage.setItem('accessToken', accessToken);
                sessionStorage.setItem('userName', response.Email);
                window.location.href = 'Data.html'
            }
            else {
                signupExternalUser(accessToken, response.LoginProvider);
                console.log(response.LoginProvider);
            }
        }
    });
}

function signupExternalUser(accessToken,provider) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            window.location.href = "/api/Account/ExternalLogin?provider="+provider+"&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A31010%2FLogin.html&state=UIprE0Btn0vnIn38Ji5YBFTcgen9SmpyFdxyMYgV2Jw1";
        }
    });
}