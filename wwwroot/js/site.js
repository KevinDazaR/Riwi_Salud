function updateCurrentTime() {
        const now = new Date();
        const formattedTime = now.toLocaleTimeString();
        document.getElementById("current-time").textContent = formattedTime;
    }
    setInterval(updateCurrentTime, 1000);
    updateCurrentTime(); // Llama la función para actualizar la hora inmediatamente

    // Text-to-Speech para pronunciar el nombre del usuario
    if ('speechSynthesis' in window) {
        var synth = window.speechSynthesis;
        var utterance = new SpeechSynthesisUtterance(document.getElementById("user-name").textContent);
        synth.speak(utterance); // Pronuncia el nombre del usuario
    } else {
        console.warn("Text-to-Speech no es compatible con este navegador");
    }