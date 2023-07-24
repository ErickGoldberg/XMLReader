import NavBar from "./Components/NavBar.js";
import Home from "./Components/Home.js";
import { store, key } from './store/Main.js';

const app = Vue.createApp({
    components: { NavBar, Home },
    template: `
    <div>
        <NavBar />
        <Home />
    </div>
    `,
});

app.use(store, key).mount("#app");
