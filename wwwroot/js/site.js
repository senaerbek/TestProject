var dataChart = []
$(".buttonSec").click(function (event) {
    $(document).ready(function () {
        var val = event.target.value;
        var myChart;
        var myLineChart;
        $.ajax({
            url: "https://localhost:5001/api/Graphic/" + val,
            type: "Get",
            success: function (data) {
                data.forEach(function (entry) {
                    dataChart.push(entry.urunFiyat)
                });
                var barChart = document.getElementById('myChart').getContext('2d');
                myChart = new Chart(barChart, {
                    type: 'bar',
                    data: {
                        labels: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran', 'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
                        datasets: [{
                            label: 'Sil',
                            data: dataChart,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });
                
                var lineChart = document.getElementById('myLineChart').getContext('2d');
                myLineChart = new Chart(lineChart, {
                    type: 'line',
                    data: {
                        labels: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran', 'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
                        datasets: [{
                            label: 'Sil',
                            data: dataChart,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });

            dataChart = []
            },

            error: function (msg) { alert(msg); }
        });
       
    });
  
});