import Table from "./Table.js";
import Modal from "./Modal.js";

export default {
    components: { Table, Modal },
    template: `
    <main>
            <div>
                <h1 class="text-center" style="margin-top:45px; margin-bottom:20px;">Lista de XMLs:</h1>
                <Modal />
                <form class="d-flex me-3" role="search" style="float: right">
                <input class="form-control me-2" v-model="filtro" type="search" placeholder="Search" style="width: 200px; " aria-label="Search">
                <button class="btn btn-outline-success" @click="enviarFiltro()" style="color:#0D6efd; border-color:#0D6efd; whidth: 50px;" type="submit">Search</button>
              </form>
            </div>

            <Table />
            <router-view></router-view>
        </main>

    `,
    name: "Home",
    data() {
        return {
            filtro: ''
        }
    },
    methods: {
        enviarFiltro() {
            console.log(this.filtro);
        }
    }
}