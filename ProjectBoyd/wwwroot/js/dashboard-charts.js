function updateCircularProgress(element, dividend) {

    const half1 = element.querySelector(".half1")
    const half2 = element.querySelector(".half2")

    const degrees = 360*dividend

    half2Rot = Math.min(180,degrees)

    half1Rot = Math.max(0, Math.min(360,degrees) - 180)

    half2.style.transform = "rotate(" + half2Rot + "deg)"

    if (half1Rot == 0) {

        half1.style.display = "none"

    } else {

        half1.style.display = "block"
        half1.style.transform = "rotate(" + -(180-half1Rot) + "deg)"

    }

}

//window.onload = function () {

//    const circularProgressBars = document.querySelectorAll(".circular-progress")
    
//    circularProgressBars.forEach( (element) => {

//        updateCircularProgress(element,0)

//    })

//}

function LoadChart(months, classesTaught) {
    const chartElement = document.getElementById("dashboard-classes-taught-graph")
    try {
        const chart = new Chart(chartElement, {
            type: "bar",
            data: {
                labels: months,
                datasets: [{
                    label: "Classes Taught",
                    data: classesTaught,

                    backgroundColor: ["#309DFF"],
                    borderRadius: "5"
                }]
            },
            options: {
                maintainAspectRatio: false,
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }

            }

        })
    } catch (exception_var) {
        console.log("Graph already exists");
    }
}

function PopulateCircularBars(amountList) {
    const circularProgressBars = document.querySelectorAll(".circular-progress")

    circularProgressBars.forEach((element) => {

        updateCircularProgress(0, amountList)

    })
}
