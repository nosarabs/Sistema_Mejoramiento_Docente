﻿//Berta Sánchez Jalet
//COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
//Tarea técnica: Mostrar las nota resultantes.
//Cumplimiento: 5/10
class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        var nMalo;
        var nRegular;
        var nBueno;

        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: {
                unidadesAcademicas: unidadesAcademicas,
                carrerasEnfasis: carrerasEnfasis,
                grupos: grupos,
                profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;

                nMalo = resultados.nMalo;
                nRegular = resultados.nRegular;
                nBueno = resultados.nBueno;
            }

        });
        return [promedio, cantidad, nMalo, nRegular, nBueno]
    }

    recuperarPromedioCursos(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        var nMalo;
        var nRegular;
        var nBueno;

        $.ajax({
            url: '/Dashboard/ObtenerPromedioCursos',
            data: {
                    unidadesAcademicas: unidadesAcademicas,
                    carrerasEnfasis: carrerasEnfasis,
                    grupos: grupos,
                    profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;

                nMalo = resultados.nMalo;
                nRegular = resultados.nRegular;
                nBueno = resultados.nBueno;
            }
        });
        return [promedio, cantidad, nMalo, nRegular, nBueno]
    }

    generarGrafico(canvas, p, nM, nR, nB) {

        Chart.pluginService.register({
            beforeDraw: function (chart) {
                if (chart.config.options.elements.center) {
                    //Get ctx from string
                    var ctx = chart.chart.ctx;

                    //Get options from the center object in options
                    var centerConfig = chart.config.options.elements.center;
                    var fontStyle = centerConfig.fontStyle || 'Arial';
                    var txt = centerConfig.text;
                    var color = centerConfig.color || '#000';
                    var sidePadding = centerConfig.sidePadding || 20;
                    var sidePaddingCalculated = (sidePadding / 100) * (chart.innerRadius * 2)
                    //Start with a base font of 30px
                    ctx.font = "60px " + fontStyle;

                    //Get the width of the string and also the width of the element minus 10 to give it 5px side padding
                    var stringWidth = ctx.measureText(txt).width;
                    var elementWidth = (chart.innerRadius * 2) - sidePaddingCalculated;

                    // Find out how much the font can grow in width.
                    var widthRatio = elementWidth / stringWidth;
                    var newFontSize = Math.floor(30 * widthRatio);
                    var elementHeight = (chart.innerRadius * 2);

                    // Pick a new font size so it will not be larger than the height of label.
                    var fontSizeToUse = Math.min(newFontSize, elementHeight);

                    //Set font settings to draw it correctly.
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'middle';
                    var centerX = ((chart.chartArea.left + chart.chartArea.right) / 2);
                    var centerY = ((chart.chartArea.top + chart.chartArea.bottom) / 2);
                    ctx.font = fontSizeToUse + "px " + fontStyle;
                    ctx.fillStyle = color;

                    //Draw text in center
                    ctx.fillText(txt, centerX, centerY);
                }
            }
        });

        var colorP;
   
        if (p >= 70) {
           colorP = "#b9d989";
        } else if (p >= 50) {
            colorP = "#8ed8f8";
        } else {
            colorP = "#ffe06a";
        }

        var config = {
            type: 'doughnut',
            data: {
                labels: [
                    "0-50",
                    "51-70",
                    "71-100"
                ],
                datasets: [{
                    data: [nM, nR, nB],
                    backgroundColor: [
                        "#ffe06a",
                        "#8ed8f8",
                        "#b9d989"

                    ],
                    hoverBackgroundColor: [
                        "#ffe06a",
                        "#8ed8f8",
                        "#b9d989"
                    ]
                }]
            },
            options: {
                elements: {
                    center: {
                        text: Math.ceil(p),
                        color: colorP, // Default is #000000
                        fontStyle: 'Arial', // Default is Arial
                        sidePadding: 20 // Defualt is 20 (as a percentage)
                    }
                }
            }
        };
        var myChart = new Chart(canvas, config);
    }
}