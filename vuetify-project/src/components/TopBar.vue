<template>
  <v-app-bar app>
    <v-toolbar class="top_bar">
      <template v-if="!isLgAndUp">
      <v-app-bar-nav-icon>
        <v-menu offset-y>
          <template v-slot:activator="{ props }">
            <v-btn icon v-bind="props">
              <v-icon class="toolbar_meny">mdi-menu</v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item to="/home">
              <v-list-item-title>Home</v-list-item-title>
            </v-list-item>
            <v-list-item to="/logfile">
              <v-list-item-title>Logfile</v-list-item-title>
            </v-list-item>
            <v-list-item to="/diagrams">
              <v-list-item-title>Diagrams</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-app-bar-nav-icon>
    </template>
      <v-toolbar-title class="toolbar_title">
        <router-link to="/home">Case Project</router-link>
      </v-toolbar-title>
    </v-toolbar>
  </v-app-bar>
</template>

<script setup>
import { useDisplay } from 'vuetify';

const { lgAndUp } = useDisplay();
const isLgAndUp = lgAndUp;
</script>

<style scoped>
.top_bar {
  background-image: url('https://www.triona.se/assets/img/red-strip.jpg');
  background-position: center;
}

.toolbar_title {
  color: white;
  font-family: 'BeBas Neue', sans-serif;
  font-size: 2rem;
}

a {
  text-decoration: none;
  color: inherit;
}
a:hover {
  text-decoration: underline;
}

.toolbar_meny {
  color: white;
}
</style>