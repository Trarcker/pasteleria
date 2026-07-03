document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('pedidoForm');
    const lista = document.getElementById('listaPedidos');
    
    let pedidos = JSON.parse(localStorage.getItem('pedidos')) || [];

    const renderizar = () => {
        lista.innerHTML = '';
        pedidos.forEach((p, index) => {
            let li = document.createElement('li');
            li.innerHTML = `${p.nombre} - ${p.cantidad} unidades 
                            <button onclick="eliminar(${index})">Eliminar</button>`;
            lista.appendChild(li);
        });
    };

    window.eliminar = (index) => {
        pedidos.splice(index, 1);
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        renderizar();
    };

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const nombre = document.getElementById('nombre').value;
        const cantidad = document.getElementById('cantidad').value;
        const email = document.getElementById('email').value; // Capturamos el email
        
        // Reglas de validación
        if (nombre.length < 3 || cantidad <= 0) {
            document.getElementById('error-msg').innerText = "Revisa nombre (>3) y cantidad (>0)";
            return;
        }

        // Nueva regla: validar que el email contenga un '@'
        if (!email.includes('@')) {
            document.getElementById('error-msg').innerText = "Correo electrónico inválido.";
            return;
        }

        // Si pasa todas las validaciones
        document.getElementById('error-msg').innerText = ""; // Limpiar error
        pedidos.push({ nombre, cantidad, email });
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        renderizar();
        form.reset();
    });

    renderizar();
});