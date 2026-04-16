# 📚 Sistema de Registro de Estudiantes

<div align="center">

![Logo UDB](https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/e39111f67a7d3f5a277265ccd0d2433f503e55cd/UDB_horizontal.png)

**PROGRAMACIÓN CON ESTRUCTURA DE DATOS — PED941**  
**Docente: Ing. Carmen Cecilia Morales**  
**PROYECTO FASE 1**

</div>

---

## INTEGRANTES

| Foto | Apellidos | Nombres | Carné |
|:----:|:----------|:--------|:-----:|
| <img src="https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/8fcae371d88ca4131ba3a2a4fc5ed3eeba846396/Daniel%20Alexander%20Carcamo%20Lopez%20CL243033.jpeg" width="80"> | Carcamo Lopez | Daniel Alexander | CL243033 |
| <img src="https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/8fcae371d88ca4131ba3a2a4fc5ed3eeba846396/Marlene%20Noemy%20L%C3%B3pez%20de%20Servando%20LQ221481.jpeg" width="80"> | López de Servando | Marlene Noemy | LQ221481 |
| <img src="https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/8fcae371d88ca4131ba3a2a4fc5ed3eeba846396/Melani%20Fernanda%20Mej%C3%ADa%20Baires%20MB242795.jpeg" width="80"> | Mejía Baires | Melani Fernanda | MB242795 |
| <img src="https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/8fcae371d88ca4131ba3a2a4fc5ed3eeba846396/Jos%C3%A9%20Rodrigo%20Merino%20Alfaro%20MA251211.jpeg" width="80"> | Merino Alfaro | José Rodrigo | MA251211 |
| <img src="https://raw.githubusercontent.com/DanyHohenheim/ProyectoPED/8fcae371d88ca4131ba3a2a4fc5ed3eeba846396/Leslie%20Alejandra%20Rivera%20Escobar%20RE251913.jpeg" width="80"> | Rivera Escobar | Leslie Alejandra | RE251913 |

**Universidad Don Bosco — Ciudadela Don Bosco, 14 de abril de 2026.**

---

## ÍNDICE

1. [Introducción](#1-introducción)
2. [Descripción del Problema](#2-descripción-del-problema)
   - 2.1 [Actividad Principal](#21-actividad-principal)
   - 2.2 [Problema por Solucionar](#22-problema-por-solucionar)
   - 2.3 [Situación Actual](#23-situación-actual)
3. [Justificación e Importancia](#3-justificación-e-importancia)
4. [Definición del Sistema](#4-definición-del-sistema)
   - 4.1 [Nombre del Sistema](#41-nombre-del-sistema)
   - 4.2 [Usuarios](#42-usuarios)
   - 4.3 [Funcionalidades Principales](#43-funcionalidades-principales)
5. [Estructura del Repositorio](#5-estructura-del-repositorio)
6. [Base de Datos](#6-base-de-datos)
7. [Tecnologías Utilizadas](#7-tecnologías-utilizadas)
8. [Instalación](#8-instalación)

---

## 1. Introducción

En el entorno universitario, el manejo eficiente de la información de los estudiantes
es fundamental para el buen funcionamiento de cualquier institución educativa.
Llevar un control manual o con herramientas inadecuadas genera errores, pérdida de
datos y tiempos de respuesta lentos ante consultas básicas como verificar si un
estudiante está activo, qué carrera cursa o cuándo fue registrado por primera vez.

El **Sistema de Registro de Estudiantes** surge como respuesta a esta necesidad.
Se trata de una aplicación desarrollada en **C#** con base de datos **SQLite**, cuyo
propósito es centralizar y ordenar el registro de estudiantes universitarios,
permitiendo realizar altas, bajas y consultas de manera rápida, segura y organizada.

Lo que distingue a este sistema de una simple tabla de datos es el uso de una
estructura de datos especializada: el **Árbol Binario de Búsqueda (ABB)**.
Mediante esta estructura, los registros se mantienen ordenados por carné en todo
momento. El recorrido **inorden** del árbol produce automáticamente un listado
ascendente sin necesidad de ordenar los datos manualmente, lo que representa una
ventaja significativa en rendimiento y claridad del código.

### ¿Qué incluye la Fase 1?

- Definición completa del sistema y sus usuarios.
- Diseño del esquema de base de datos relacional (SQLite).
- Implementación del módulo de gestión de estudiantes (alta, baja, consulta, actualización).
- Implementación del Árbol Binario de Búsqueda como estructura central de ordenamiento.
- Sistema de auditoría que registra cada movimiento realizado sobre los registros.

---

## 2. Descripción del Problema

### 2.1 Actividad Principal

Como equipo, identificamos la necesidad de contar con un sistema que permita
gestionar el registro de estudiantes universitarios de forma centralizada y
ordenada. El sistema está orientado a operadores administrativos que necesitan
consultar, registrar y dar de baja estudiantes de manera ágil, manteniendo
siempre un historial claro de los cambios realizados.

### 2.2 Problema por Solucionar

Sin una herramienta adecuada, quienes gestionan estos registros enfrentan
los siguientes problemas:

- Dificultad para localizar un estudiante específico entre cientos de registros.
- Sin trazabilidad: no se sabe quién realizó cada cambio ni cuándo.
- Riesgo de duplicar carnés o perder registros al eliminar datos directamente.
- Imposibilidad de generar listados ordenados de forma automática y eficiente.

### 2.3 Situación Actual

Actualmente no existe un sistema centralizado para este propósito. Los registros
se manejan de forma dispersa, sin control de roles ni auditoría de movimientos,
lo que dificulta la supervisión y genera inconsistencias en los datos. Como equipo,
desarrollamos este sistema para resolver esa problemática de manera práctica
y estructurada, aplicando los conocimientos adquiridos en la materia.

---

## 3. Justificación e Importancia

El sistema es relevante por tres razones fundamentales:

**Eficiencia:** El uso del Árbol Binario de Búsqueda permite que las operaciones
de búsqueda, inserción y eliminación se realicen en tiempo O(log n) en promedio,
frente a O(n) de una lista simple o arreglo no ordenado.

**Integridad:** La base de datos relacional con llaves foráneas y restricciones
`CHECK` garantiza que no existan registros huérfanos, carnés duplicados ni
valores inválidos en campos críticos como el rol de usuario o el tipo de movimiento.

**Trazabilidad:** Cada alta, baja o modificación queda registrada automáticamente
en la tabla `MovimientosEstudiante` con fecha exacta, usuario responsable y
descripción del cambio realizado, lo que permite auditar cualquier operación en el sistema.

---

## 4. Definición del Sistema

### 4.1 Nombre del Sistema

> **Sistema de Registro de Estudiantes**

Aplicación de escritorio desarrollada en **C#** que gestiona el registro integral
de estudiantes universitarios. Utiliza un **Árbol Binario de Búsqueda** como
estructura de datos central para mantener los registros ordenados por carné
en todo momento, permitiendo búsquedas eficientes y listados ordenados mediante
recorrido inorden.

### 4.2 Usuarios

| Rol | Descripción | Permisos |
|:----|:------------|:---------|
| **Administrador** | Control total del sistema. | Gestión de usuarios, carreras y estudiantes. Acceso completo al historial de movimientos. |
| **Operador** | Usuario de operación diaria. | Altas, bajas, actualizaciones y consultas de estudiantes. |

### 4.3 Funcionalidades Principales

1. **Alta de estudiantes** — Registro de nuevos estudiantes con carné único y carrera asignada.
2. **Baja de estudiantes** — Desactivación lógica del registro (no se elimina físicamente), con trazabilidad del movimiento.
3. **Consulta y listado ordenado** — Búsqueda por carné o ID. Listado general ordenado automáticamente mediante recorrido inorden del ABB.
4. **Actualización de datos** — Modificación de información personal con registro del cambio.
5. **Gestión de carreras** — Administración del catálogo de carreras disponibles para asignación.
6. **Auditoría de movimientos** — Registro automático de cada Alta, Actualización y Baja con fecha, usuario responsable y descripción.

---

## 5. Estructura del Repositorio
