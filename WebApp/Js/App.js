


const app = Vue.createApp({
    components: { Table },
    template: `
    <main>
        <h1>Lista de XMLs</h1>
        <Table />
    </main>
    
    `
});

app.mount("#app")