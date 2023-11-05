import React from "react";
import Chart from "chart.js/auto";
import { Line } from "react-chartjs-2";

const labels = ["January", "February", "March", "April", "May", "June", "July"];

const data = {
  labels: labels,
  datasets: [
    {
      label: "",
      fill: true,
      backgroundColor: "rgb(77, 147, 201)",
      borderColor: "rgb(22, 61, 92)",
      data: [200, 250, 415, 350, 380, 430, 545],
    },
    // {
    //   label: "",
    //   fill: true,
    //   backgroundColor: "rgb(222, 219, 78)",
    //   borderColor: "rgb(222, 219, 78)",
    //   data: [100, 450, 215, 650, 480, 440, 745],
    // },
  ],
};

const LineChart = () => {
  return (
    <div >
      <Line data={data} />
    </div>
  );
};

export default LineChart;
