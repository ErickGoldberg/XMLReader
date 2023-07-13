import NavBar from "./Components/NavBar.js";
import Table from "./Components/Table.js";
import { store, key } from './store/Main.js'

const app = Vue.createApp({
    components: { NavBar , Table },
    template: `
    <NavBar />
    <main>
        <div>
            <h1 class="text-center" style="margin-top:45px; margin-bottom:20px;">Lista de XMLs:</h1>
        </div>
        <Table />
    </main>
    
    `,

});

app.use(store).mount("#app")