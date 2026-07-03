document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('pedidoForm');
    const lista = document.getElementById('listaPedidos');
    
    // Cargar datos
    let pedidos = JSON.parse(localStorage.getItem('pedidos')) || [];
    pedidos.forEach(p => {
        let li = document.createElement('li');
        li.textContent = `${p.nombre} - ${p.cantidad} unidades`;
        lista.appendChild(li);
    });

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const nombre = document.getElementById('nombre').value;
        const cantidad = document.getElementById('cantidad').value;
        
        // Validación mínima
        if (nombre.length < 3 || cantidad <= 0) {
            document.getElementById('error-msg').innerText = "Datos inválidos";
            return;
        }

        const nuevo = { nombre, cantidad };
        pedidos.push(nuevo);
        localStorage.setItem('pedidos', JSON.stringify(pedidos));
        location.reload(); // Refrescar para ver el cambio
    });
});