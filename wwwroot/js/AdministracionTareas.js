var renderTasks;
document.addEventListener("DOMContentLoaded", function () {
    var tasks;
   
    renderTasks = (status) => {
        const taskListContainer = document.getElementById(`${status.toLowerCase()}-tasks`);
        const tasksByStatus = tasks.filter(task => task.status === status);

        taskListContainer.innerHTML = "";
        tasksByStatus.forEach(task => {
            const taskCard = document.createElement("div");
            taskCard.classList.add("card", "mb-3");

            switch (task.importance) {
                case 1:
                    taskCard.style.backgroundColor = "#ff6666";
                    break;
                case 2:
                    taskCard.style.backgroundColor = "#e67e22";
                    break;
                case 3:
                    taskCard.style.backgroundColor = "#2ecc71";
                    break;
                default:
                    taskCard.style.backgroundColor = "#f5f5f5";
            }

            // Contenido de la tarjeta
            const cardBody = document.createElement("div");
            cardBody.classList.add("card-body");

            // Título de la tarea
            const titleElement = document.createElement("h5");
            titleElement.classList.add("card-title");
            titleElement.textContent = task.name;
            cardBody.appendChild(titleElement);

            // Usuario relacionado
            const userElement = document.createElement("p");
            userElement.classList.add("card-text");
            userElement.textContent = `Usuario: ${task.user}`;
            cardBody.appendChild(userElement);

            taskCard.appendChild(cardBody);

            // Agregar clase para hacer la tarjeta clickeable
            taskCard.classList.add("clickable-task");
            taskCard.addEventListener("click", () => handleTaskClick(task.id));

            taskListContainer.appendChild(taskCard);
        });
    };


    const handleTaskClick = (taskId) => {
        
        fetch(`/TareasAPI/GetTareaById/${taskId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Error al obtener la tarea por ID.");
                }
                return response.json();
            })
            .then(data => {

                let idTarea = document.getElementById("idTareaSeleccionada");
                idTarea.value = data.tarea.id;
                document.getElementById("nombreTarea").innerHTML = "Tarea: " + data.tarea.name;
                document.getElementById("DescripcionTarea").innerHTML = "Detalle: " + data.tarea.description;
                document.getElementById("UsuarioTarea").innerHTML = "Usuario: " + data.tarea.user;
                let estado = document.getElementById("EstadoTarea");
                switch (data.tarea.status) {
                    case "TODO":
                        estado.innerHTML = "Estado: TO DO";
                        break;
                    case "INPROGRESS":
                        estado.innerHTML = "Estado: IN PROGRESS";
                        break;
                    default: estado.innerHTML = "Estado: "+data.tarea.status;
                }


                abrirModal();
                console.log(data);
            })
            .catch(error => {
                console.error("Error en la solicitud:", error);
            });
    };

    fetch("/TareasAPI/GetTareas") 
        .then(response => response.json())
        .then(data => {
            tasks = data.tareas;
            renderTasks("TODO");
            renderTasks("INPROGRESS");
            renderTasks("REVIEWING");
            renderTasks("DONE");
        })
        .catch(error => console.error("Error al obtener las tareas:", error));
   
});

function renderAllTasks() {
    alert("rederAll");
    renderTasks("TODO");
    renderTasks("INPROGRESS");
    renderTasks("REVIEWING");
    renderTasks("DONE");
}

function cerrarModal() {
    let modal = document.getElementById("ModalDetalleTarea");
    modal.classList.remove("show");
    modal.style.display = "none";
    modal.setAttribute("aria-hidden", "true");

    document.body.classList.remove("modal-open");
}


function abrirModal() {
    let modal = document.getElementById("ModalDetalleTarea");
    modal.classList.add("show");
    modal.style.display = "block";
    modal.setAttribute("aria-hidden", "false");
    document.body.classList.add("modal-open");
}

function eliminarTarea() {
    let idTarea = document.getElementById("idTareaSeleccionada").value;
    try {
        alert(idTarea);
        const response = await fetch("/TareasAPI/DeleteTarea", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: "id=" + idTarea // Enviar el valor de idTarea directamente en el cuerpo de la solicitud
        });

        const data = await response.json();
        renderAllTasks();

        if (data != true) {
            alert("No se logró eliminar la tarea seleccionada");
        }
    } catch (error) {
        console.error("Error al llamar al controlador:", error);
    }
}

async function avanzarTarea() {
    let idTarea = document.getElementById("idTareaSeleccionada").value;
    try {
        alert(idTarea);
        const response = await fetch("/TareasAPI/AvanzarTarea", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded" 
            },
            body: "id=" + idTarea // Enviar el valor de idTarea directamente en el cuerpo de la solicitud
        });

        const data = await response.json();
        renderAllTasks();

        if (data != true) {
            alert("No se logró actualizar el estado de la tarea seleccionada");
        }
    } catch (error) {
        console.error("Error al llamar al controlador:", error);
    }
}

