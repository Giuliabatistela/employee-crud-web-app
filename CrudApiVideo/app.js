//Função para verificar se o usuário está authenticado

function checkAuthentication() {
    //Verifica se o usuário esta autenticado,comparando com um valor no localStorage
    const isAuthenticated = localStorage.getItem('authenticated');

    //Se isAuthenticated for verdadeiro, o usuário está autenticado
    return isAuthenticated === 'true';
}

//Verifica a autenticação antes de permitir acesso a página 
window.addEventListener('DOMContentLoaded', () => {
    if (!checkAuthentication()) {
        window.location.href = 'login.html'; // Redireciona para a página de login se não estiver autenticado
    }
});

function menu() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}