
const R = localStorage.getItem('rol')
const U = localStorage.getItem('usuario')
const name = localStorage.getItem('name')
var saludo = document.getElementById('saludo');

var DivMenu = document.getElementById('menuDin');
var ContMenu = document.getElementById('ContMenu');

var ul;
var li;
var a;
var Amenu;
var Div;

const CrearUl = (clase) => {
    ul = document.createElement('ul');
    ul.setAttribute('class', clase);
}

const CrearLi = (clase) => {
    li = document.createElement('li');
    li.setAttribute('class', clase);
}

const CrearA = (clase, ref) => {
    a = document.createElement('a');
    a.setAttribute('class', clase);
    a.setAttribute('href', ref);
}

const CrearDiv = (clase) => {
    Div = document.createElement('div');
    Div.setAttribute('class', clase)
}


window.onload = function () {
    
}


if (R == "Administrador" || R == "Campesino" || R == "Cliente") {

    saludo.innerText = '!Hola ' + name + '¡';

    let parametro = {
        "rol": R
    }

    $.ajax({
        cache: false,
        url: '/Inicio/ConsultarPrivilegio',
        async: false,
        type: "GET",
        dataType: "json",
        data: parametro,
        error: function (response, status, error) {
            alert(error);
        },

        success: function (response) {

            CrearDiv('menu');
            CrearUl('nav nav-pills');

            response.forEach(function (item, index) {
                console.log(item)
                CrearLi('nav-item');
                if (R == "Administrador") {
                    CrearA('nav-link', '/PanelAdmin/' + item);
                }
                if (R == "Campesino") {
                    CrearA('nav-link', '/PanelVendedor/' + item);
                }
                if (R == "Cliente") {
                    CrearA('nav-link', '/PanelUser/' + item);
                }

                a.innerText = item;
                ul.appendChild(a);
                Div.appendChild(ul);

                DivMenu.appendChild(Div);
            });

            CrearA('nav-link', '#');
            a.innerText = "Cerrar sesion"
            li.appendChild(a);
            ul.appendChild(li);
            Div.appendChild(ul);


            ContMenu.appendChild(Div);

            a.addEventListener('click', function () {
                localStorage.removeItem('rol');
                localStorage.removeItem('usuario');
                //location.reload();
                window.location = '/Inicio/index'
            });


        },

    });


} else {

    CrearUl('nav nav-pills');

    for (var i = 0; i <= 1; i++) {


        CrearLi();
        if (i == 0) {
            CrearA('nav-link', '/Registrar/registrar');
            a.innerText = 'Registrarse'
        } else {
            CrearA('nav-link', '/Login/login');
            a.innerText = 'Iniciar sesión'
        }
        li.appendChild(a);
        ul.appendChild(li);

    }

    DivMenu.appendChild(ul);


}



