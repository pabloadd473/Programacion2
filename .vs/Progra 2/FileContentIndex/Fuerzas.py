import matplotlib.pyplot as plt # type: ignore
import numpy as np

# Función para descomponer vectores
def descomponer(magnitud, angulo_grados):
    angulo_rad = np.radians(angulo_grados)
    fx = magnitud * np.cos(angulo_rad)
    fy = magnitud * np.sin(angulo_rad)
    return fx, fy

# Datos de las fuerzas
fuerzas = {
    'F1': {'mag': 33, 'angle': 28, 'color': 'blue'},
    'F2': {'mag': 44, 'angle': 95, 'color': 'green'},
    'F3': {'mag': 55, 'angle': 295, 'color': 'orange'},
    'R':  {'mag': 49.45, 'angle': 11.00, 'color': 'red'},       # Resultante
    'E':  {'mag': 49.45, 'angle': 191.00, 'color': 'purple'}    # Equilibrante
}

# Crear gráfico
plt.figure(figsize=(8, 8))
plt.axhline(0, color='gray', lw=0.5)
plt.axvline(0, color='gray', lw=0.5)

# Dibujar vectores
for nombre, datos in fuerzas.items():
    fx, fy = descomponer(datos['mag'], datos['angle'])
    plt.quiver(0, 0, fx, fy, angles='xy', scale_units='xy', scale=1, color=datos['color'], label=nombre)

# Configurar ejes
plt.xlim(-60, 60)
plt.ylim(-60, 60)
plt.gca().set_aspect('equal')
plt.grid(True)
plt.legend()
plt.title('Sistema de Fuerzas, Resultante y Equilibrante')
plt.xlabel('Fx (N)')
plt.ylabel('Fy (N)')

plt.tight_layout()
plt.show()
