/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins';

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'

import vuetify from './plugins/vuetify';

import router from './router'

const app = createApp(App)
app.use(vuetify)

app.use(router)

registerPlugins(app)

app.mount('#app')
