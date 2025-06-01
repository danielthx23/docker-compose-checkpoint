// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Função para confirmar exclusão
function confirmarExclusao(id, nome) {
    if (confirm(`Deseja realmente excluir a categoria "${nome}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Inicialização de tooltips do Bootstrap
document.addEventListener('DOMContentLoaded', function () {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
});
