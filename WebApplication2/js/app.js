
window.addEventListener('load', function () {
    new Glider(document.querySelector('.car_lista'), {
        slidesToShow: 4,
        slidesToScroll: 2,
        dots: '.car_indicadores',
        arrows: {
          prev: '.anterior',
          next: '.siguiente'
        }
    });
});