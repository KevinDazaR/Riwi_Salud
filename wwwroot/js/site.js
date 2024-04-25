// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Función para obtener la hora actual en formato deseado
function getCurrentTime() {
    const now = new Date(); // Obtiene la hora actual del sistema
    // Formato de hora (HH:mm:ss)
    const formattedTime = now.toLocaleTimeString(); 
    return formattedTime;
}

// Función para actualizar el contenido del elemento con la hora actual
function updateCurrentTime() {
    const timeElement = document.getElementById("current-time");
    if (timeElement) {
        timeElement.textContent = getCurrentTime(); // Actualiza el contenido del elemento
    }
}

// Llama a updateCurrentTime inmediatamente para mostrar la hora actual al cargar la página
updateCurrentTime();

// Usa setInterval para actualizar la hora cada segundo
setInterval(updateCurrentTime, 1000); // Actualiza cada segundo (1000 ms)
