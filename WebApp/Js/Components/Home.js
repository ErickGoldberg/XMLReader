import Table from "./Table.js";

export default {
    components: { Table },
    template: `
    <main>
        <div>
            <h1 class="text-center" style="margin-top:45px; margin-bottom:20px;">Lista de XML&apos;s:</h1>   
        </div>
        <Table />
        <router-view></router-view>
    </main>
    `, 
    name: "Home"
}
    