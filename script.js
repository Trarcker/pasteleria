document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('pedidoForm');
    const lista = document.getElementById('listaPedidos');
    const select = document.getElementById('pastelSelect');
    
    let pedidos = JSON.parse(localStorage.getItem('pedidos')) || [];

    // 1. Cargar menú desde JSON
    fetch('data.json')
        .then(response => response.json())
        .then(data => {
            select.innerHTML = ''; 
            data.forEach(pastel => {
                let option = document.createElement('option');
                option.value = pastel.nombre;
                option.textContent = `${pastel.nombre} - $${pastel.precio}`;
                select.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error al cargar el JSON:', error);
            select.innerHTML = '<option value="">Error al cargar menú</option>';
        });

    // 2. Renderizar pedidos en el DOM
    const renderizar = () => {
        lista.innerHTML = '';
        pedidos.forEach((p, index) => {
            let li = document.createElement('li');
            li.innerHTML = `${p.nombre} (${p.pastel}) - ${p.cantidad} unidades 
                            <button onclick="eliminar(${index})">Eliminar</button>`;
            lista.appendChild(li);
        });
    };

    // 3. Función eliminar
    window.eliminar = (index) => {
        pedidos.splice(index, 1);
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        renderizar();
    };

    // 4. Lógica de envío
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const nombre = document.getElementById('nombre').value;
        const cantidad = document.getElementById('cantidad').value;
        const email = document.getElementById('email').value;
        const pastel = document.getElementById('pastelSelect').value;
        
        if (nombre.length < 3 || cantidad <= 0) {
            document.getElementById('error-msg').innerText = "Revisa nombre (>3) y cantidad (>0)";
            return;
        }

        if (!email.includes('@')) {
            document.getElementById('error-msg').innerText = "Correo electrónico inválido.";
            return;
        }

        document.getElementById('error-msg').innerText = "";
        pedidos.push({ nombre, cantidad, email, pastel });
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        renderizar();
        form.reset();
    });

    renderizar();
});