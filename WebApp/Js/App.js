import Table from './Components/Table.js';

const app = Vue.createApp({
    template: `
    <main>
      <div></div>
      <h1>Lista de XMLs</h1>
      <Table />
    </main>
  `
});

app.mount("#app");
