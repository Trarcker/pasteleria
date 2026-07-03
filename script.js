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
        
        if (nombre.length < 3 || cantidad <= 0) {
            document.getElementById('error-msg').innerText = "Revisa los campos (nombre > 3, cantidad > 0)";
            return;
        }

        pedidos.push({ nombre, cantidad });
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        renderizar();
        form.reset();
    });

    renderizar();
});