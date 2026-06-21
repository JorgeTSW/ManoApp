const videoElement = document.getElementById('webcam');
const canvasElement = document.getElementById('overlay');
const canvasCtx = canvasElement.getContext('2d');
const resultDiv = document.getElementById('result');

let lastSentTime = 0;
const SEND_INTERVAL_MS = 300;

const hands = new Hands({
    locateFile: (file) => `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`
});

hands.setOptions({
    maxNumHands: 2,
    modelComplexity: 1,
    minDetectionConfidence: 0.7,
    minTrackingConfidence: 0.5
});

hands.onResults(onResults);

function onResults(results) {
    canvasCtx.save();
    canvasCtx.clearRect(0, 0, canvasElement.width, canvasElement.height);

    if (results.multiHandLandmarks && results.multiHandLandmarks.length > 0) {
        for (let i = 0; i < results.multiHandLandmarks.length; i++) {
            const landmarks = results.multiHandLandmarks[i];
            drawConnectors(canvasCtx, landmarks, Hands.HAND_CONNECTIONS, { color: '#00FF00', lineWidth: 2 });
            drawLandmarks(canvasCtx, landmarks, { color: '#FF0000', radius: 3 });
        }

        const now = Date.now();
        if (now - lastSentTime > SEND_INTERVAL_MS) {
            lastSentTime = now;
            sendLandmarksToServer(results.multiHandLandmarks, results.multiHandedness);
        }
    } else {
        resultDiv.innerText = 'No se detecta ninguna mano';
    }

    canvasCtx.restore();
}

function sendLandmarksToServer(multiHandLandmarks, multiHandedness) {
    const hands = multiHandLandmarks.map((landmarks, index) => {
        const rawLabel = multiHandedness[index].label;
        const correctedLabel = rawLabel === 'Left' ? 'Right' : 'Left';

        return {
            handedness: correctedLabel,
            score: multiHandedness[index].score,
            landmarks: landmarks.map(point => ({
                x: 1 - point.x,
                y: point.y,
                z: point.z
            }))
        };
    });

    fetch('https://localhost:7088/api/Hand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(hands)
    })
        .then(response => response.json())
        .then(data => {
            if (data.length === 0) {
                resultDiv.innerText = 'Sin resultado';
                return;
            }
            const texto = data.map(r => `${r.handedness}: ${r.gestureName} (${r.fingerCount} dedos)`).join(' | ');
            resultDiv.innerText = texto;
        })
        .catch(error => {
            console.error('Error al llamar al backend:', error);
            resultDiv.innerText = 'Error al procesar';
        });
}

const camera = new Camera(videoElement, {
    onFrame: async () => {
        await hands.send({ image: videoElement });
    },
    width: 640,
    height: 480
});

camera.start();