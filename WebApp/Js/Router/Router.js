import About from "../components/About.js";
import Table from "../Components/Table.js";

const routes = [
    {
        path: "/",
        component: Table

    },
    {
        path: "/about",
        component: About
    }
]

const router = VueRouter.createRouter({
    history: VueRouter.createWebHashHistory(),
    routes
})

export default router;