import { createStore } from 'vuex';
import { RETURN_XMLS, GET_XMLS } from './Consts';
import http from '../protocols/AxiosConnection';

export const key = Symbol();

const state = {
    xmls: [],
};

const mutations = {
    [GET_XMLS](state, listaDeXmls) {
        state.xmls = listaDeXmls;
        console.log(listaDeXmls);
    },
};

const actions = {
    async [RETURN_XMLS]({ commit }) {
        try {
            const response = await http.get('/file');
            console.log(response.data);
            commit(GET_XMLS, response.data);
        } catch (error) {
            console.error(error);
        }
    },
};

const store = createStore({
    state,
    mutations,
    actions,
});

export default store;
