// import './plugins/axios'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import axios from "axios";
import mitt from 'mitt';
import VueCookies from 'vue-cookies'

const emitter = mitt();
loadFonts()

var app = createApp(App)
app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$axios = axios;
app.use(router)
    .use(vuetify)
    .use(VueCookies)
    .use(axios).mount('#app');