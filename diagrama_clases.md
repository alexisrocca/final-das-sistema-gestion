# Diagramas de Clases - Sistema de Gestion

## 1. Jerarquia de Herencia

```mermaid
---
config:
  layout: dagre
  theme: default
  class:
    hideEmptyMembersBox: true
  look: neo
---
classDiagram
    direction TB

    class BaseEntity {
        +string Id
        +bool Activo
        +Habilitar() void
        +Deshabilitar() void
    }

    class NamedEntity {
        +string Id
        -string _nombre
        +string Nombre
    }

    class Producto {
        +int StockActual
        +string Descripcion
        +decimal PrecioUnitario
        +string Categoria
        +bool Vencimiento
        +decimal PrecioCompraPromedio
        +DateTime? FechaVencimiento
        +ActualizarPrecioCompraPromedio(decimal, int) void
    }

    class Categoria {
        +string Descripcion
    }

    class Proveedor {
        +string Tel
        +string Email
        +string Direccion
    }

    class Sucursal {
        +string Direccion
        +string Telefono
    }

    class Vendedor {
        +string SucursalId
        +string Legajo
    }

    class Cliente {
        <<abstract>>
        +string Email
        +string DniCuit
        +string Telefono
        +string Direccion
        +ObtenerLimiteDescuento()* decimal
    }

    class ClienteMinorista {
        +ObtenerLimiteDescuento() decimal
    }

    class ClienteMayorista {
        +ObtenerLimiteDescuento() decimal
    }

    class Lote
    class MovimientoStock
    class Venta
    class DetalleVenta
    class MovimientoCuentaCorriente

    BaseEntity <|-- NamedEntity
    BaseEntity <|-- Lote
    BaseEntity <|-- MovimientoStock
    BaseEntity <|-- Venta
    BaseEntity <|-- DetalleVenta
    BaseEntity <|-- MovimientoCuentaCorriente
    NamedEntity <|-- Producto
    NamedEntity <|-- Categoria
    NamedEntity <|-- Proveedor
    NamedEntity <|-- Sucursal
    NamedEntity <|-- Vendedor
    NamedEntity <|-- Cliente
    Cliente <|-- ClienteMinorista
    Cliente <|-- ClienteMayorista
```

## 2. Ventas y Clientes

```mermaid
---
config:
  layout: dagre
  theme: default
  class:
    hideEmptyMembersBox: true
  look: neo
---
classDiagram
    direction LR

    class Venta {
        +string Id
        +bool Activo
        +string ClienteId
        +string VendedorId
        +DateTime Fecha
        +decimal DescuentoAplicado
        +MetodoPago MetodoPago
        +List~DetalleVenta~ Detalles
        +decimal MontoTotal
        +AgregarDetalle(DetalleVenta) void
    }

    class DetalleVenta {
        +string Id
        +bool Activo
        +string ProductoId
        +int Cantidad
        +decimal PrecioUnitario
        +decimal Subtotal
    }

    class Cliente {
        <<abstract>>
        +string Id
        +string Nombre
        +bool Activo
        +string Email
        +string DniCuit
        +string Telefono
        +string Direccion
        +ObtenerLimiteDescuento()* decimal
    }

    class ClienteMinorista {
        +ObtenerLimiteDescuento() decimal
    }

    class ClienteMayorista {
        +ObtenerLimiteDescuento() decimal
    }

    class Vendedor {
        +string Id
        +string Nombre
        +bool Activo
        +string SucursalId
        +string Legajo
    }

    class Producto {
        +string Id
        +string Nombre
        +bool Activo
        +string Descripcion
        +decimal PrecioUnitario
        +int StockActual
        +string Categoria
    }

    class MetodoPago {
        <<enumeration>>
        Efectivo
        Tarjeta
        Transferencia
        CuentaCorriente
    }

    Cliente <|-- ClienteMinorista
    Cliente <|-- ClienteMayorista

    Venta "*" --> "1" Cliente : cliente
    Venta "*" --> "1" Vendedor : vendedor
    Venta "1" *-- "*" DetalleVenta : detalles
    DetalleVenta "*" --> "1" Producto : producto
    Venta ..> MetodoPago : usa
```

## 3. Inventario y Stock

> **Nota:** `Producto.Categoria` en realidad es un `string` (nombre de la categoria), no una FK a la entidad `Categoria`. No existe relacion directa tecnicamente en el modelo EF entre ambas.

```mermaid
---
config:
  layout: dagre
  theme: default
  class:
    hideEmptyMembersBox: true
  look: neo
---
classDiagram
    direction LR

    class Producto {
        +string Id
        +string Nombre
        +bool Activo
        +string Descripcion
        +decimal PrecioUnitario
        +int StockActual
        +string Categoria
        +bool Vencimiento
        +decimal PrecioCompraPromedio
        +DateTime? FechaVencimiento
        +ActualizarPrecioCompraPromedio(decimal, int) void
    }

    class Categoria {
        +string Id
        +string Nombre
        +bool Activo
        +string Descripcion
    }

    class Proveedor {
        +string Id
        +string Nombre
        +bool Activo
        +string Tel
        +string Email
        +string Direccion
    }

    class Sucursal {
        +string Id
        +string Nombre
        +bool Activo
        +string Direccion
        +string Telefono
    }

    class Vendedor {
        +string Id
        +string Nombre
        +bool Activo
        +string SucursalId
        +string Legajo
    }

    class Lote {
        +string Id
        +bool Activo
        +string IdProducto
        +string IdProveedor
        +DateTime FechaIngreso
        +DateTime FechaVencimiento
        +int Cantidad
        +decimal PrecioCompra
        +string SucursalId
    }

    class MovimientoStock {
        +string Id
        +bool Activo
        +string IdProducto
        +int Cantidad
        +DateTime Fecha
        +string IdProveedor
        +TipoMovimiento TipoMovimiento
        +TipoEgreso? TipoEgreso
        +string IdLote
        +decimal? PrecioCompra
    }

    class TipoMovimiento {
        <<enumeration>>
        Ingreso = 1
        Egreso = 2
    }

    class TipoEgreso {
        <<enumeration>>
        Venta = 1
        Merma = 2
        Devolucion = 3
        Otro = 4
    }

    Producto "1" --> "*" Lote : tiene lotes
    Producto "1" --> "*" MovimientoStock : registra movimientos
    Lote "*" --> "1" Sucursal : almacenado en
    Lote "*" --> "0..1" Proveedor : provisto por
    MovimientoStock "*" --> "0..1" Proveedor : proveido por
    Vendedor "*" --> "1" Sucursal : trabaja en
    MovimientoStock ..> TipoMovimiento : usa
    MovimientoStock ..> TipoEgreso : usa
```

## 4. Cuenta Corriente

```mermaid
---
config:
  layout: dagre
  theme: default
  class:
    hideEmptyMembersBox: true
  look: neo
---
classDiagram
    direction LR

    class MovimientoCuentaCorriente {
        +string Id
        +bool Activo
        +string ClienteId
        +DateTime Fecha
        +TipoMovimientoCuenta Tipo
        +decimal Monto
        +string Descripcion
        +string VentaId
    }

    class Cliente {
        <<abstract>>
        +string Id
        +string Nombre
        +bool Activo
        +string Email
        +string DniCuit
        +string Telefono
        +string Direccion
        +ObtenerLimiteDescuento()* decimal
    }

    class ClienteMinorista {
        +ObtenerLimiteDescuento() decimal
    }

    class ClienteMayorista {
        +ObtenerLimiteDescuento() decimal
    }

    class Venta {
        +string Id
        +bool Activo
        +string ClienteId
        +string VendedorId
        +DateTime Fecha
        +decimal DescuentoAplicado
        +MetodoPago MetodoPago
        +decimal MontoTotal
    }

    class TipoMovimientoCuenta {
        <<enumeration>>
        Debito
        Credito
    }

    Cliente <|-- ClienteMinorista
    Cliente <|-- ClienteMayorista

    MovimientoCuentaCorriente "*" --> "1" Cliente : cliente
    MovimientoCuentaCorriente "*" --> "0..1" Venta : venta origen
    MovimientoCuentaCorriente ..> TipoMovimientoCuenta : usa
```
