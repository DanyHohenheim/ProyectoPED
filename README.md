# 📚 UNIVERSIDAD DON BOSCO (CAMPUS VIRTUAL)

<div align="center">

![Logo UDB](<!-- AQUÍ VA EL LOGO -->)

**ANÁLISIS Y DISEÑO DE SISTEMAS**  
**Docente: Juan Miranda**  
**PROYECTO FASE 1**

</div>

---

## INTEGRANTES

| Apellidos | Nombres | Carné |
|:----------|:--------|:-----:|
| Alvarenga Claros | Luis Eduardo | AC250260 |
| Duran Linares | Ricardo Enrique | DL251291 |
| García Ramos | Francisco Daniel | GR250170 |
| Miranda Rodriguez | Angel Joel | MR241687 |
| Rivera Escobar | Leslie Alejandra | RE251913 |

**Ciudadela Don Bosco, 19 de marzo de 2026.**

---

## ÍNDICE

1. [Introducción](#1-introducción)
2. [Descripción del Problema](#2-descripción-del-problema)
3. [Justificación e Importancia](#3-justificación-e-importancia)
4. [Definición del Sistema](#4-definición-del-sistema)
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
estudiante está activo, qué carrera cursa o cuándo fue registrado.

El **Sistema de Registro de Estudiantes** surge como respuesta a esta necesidad. 
Se trata de una aplicación desarrollada en C# con base de datos SQLite, cuyo 
propósito es centralizar y ordenar el registro de estudiantes universitarios, 
permitiendo realizar altas, bajas y consultas de manera rápida, segura y organizada.

Lo que distingue a este sistema de una simple tabla de datos es el uso de una 
estructura de datos especializada: el **Árbol Binario de Búsqueda (ABB)**. 
Mediante esta estructura, los registros se mantienen ordenados por carné en todo 
momento. El recorrido inorden del árbol produce automáticamente un listado ascendente 
sin necesidad de ordenar los datos manualmente, lo que representa una ventaja 
significativa en rendimiento y claridad del código.

### ¿Qué incluye la Fase 1?

- Definición completa del sistema y sus usuarios.
- Diseño del esquema de base de datos relacional.
- Implementación del módulo de gestión de estudiantes (alta, baja, consulta).
- Implementación del Árbol Binario de Búsqueda como estructura central.
- Sistema de auditoría que registra cada movimiento realizado.

---

## 2. Descripción del Problema

### 2.1 Actividad Principal

La Universidad Don Bosco necesita un sistema que permita a sus operadores 
administrativos gestionar el padrón de estudiantes activos e inactivos por carrera, 
manteniendo un historial de cambios y garantizando que los datos estén siempre 
ordenados y disponibles para consulta.

### 2.2 Problema por Solucionar

Sin una herramienta adecuada, los administradores enfrentan:

- Dificultad para localizar estudiantes entre cientos de registros.
- Sin trazabilidad de quién realizó cada cambio ni cuándo.
- Riesgo de duplicar carnés o perder registros al eliminar datos.
- Imposibilidad de generar listados ordenados de forma automática.

### 2.3 Situación Actual

Actualmente no existe un sistema centralizado para este fin. Los registros 
se llevan de forma dispersa, sin control de usuarios ni auditoría de movimientos, 
lo que dificulta la supervisión y genera inconsistencias en los datos.

---

## 3. Justificación e Importancia

El sistema es relevante por tres razones principales:

**Eficiencia:** El uso del Árbol Binario de Búsqueda permite que las operaciones 
de búsqueda, inserción y eliminación se realicen en tiempo O(log n) en promedio, 
frente a O(n) de una lista simple.

**Integridad:** La base de datos relacional con llaves foráneas y restricciones 
garantiza que no existan registros huérfanos ni carnés duplicados.

**Trazabilidad:** Cada alta, baja o modificación queda registrada en la tabla 
de movimientos con fecha, usuario responsable y descripción del cambio.

---

## 4. Definición del Sistema

**Nombre:** Sistema de Registro de Estudiantes  
**Estructura central:** Árbol Binario de Búsqueda (ordenamiento por carné)

### Usuarios

| Rol | Descripción |
|:----|:------------|
| **Administrador** | Control total: gestiona usuarios, carreras y estudiantes. |
| **Operador** | Gestiona altas, bajas y consultas de estudiantes. |

### Funcionalidades Principales

1. Alta de estudiantes con carné único y carrera asignada.
2. Baja lógica de estudiantes (no se eliminan físicamente).
3. Consulta y listado ordenado por carné mediante recorrido inorden del ABB.
4. Actualización de datos personales con trazabilidad.
5. Gestión del catálogo de carreras.
6. Auditoría automática de movimientos (Alta, Actualización, Baja).

---

## 5. Estructura del Repositorio
