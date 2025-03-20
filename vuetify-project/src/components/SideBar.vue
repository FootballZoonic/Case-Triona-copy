<template>
     <v-navigation-drawer class="custom-sidebar">
      <v-list>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="sidebar-text"><router-link to="/logfile">Logfile</router-link></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="sidebar-text"><router-link to="/diagrams">Diagrams</router-link></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
</template>
<script>
</script>
<style scoped>
a {
  text-decoration: none;
  color: inherit;
  background: none;
  padding: 0;
  margin: 0;
  border: none;
  font-family: 'Montserrat', sans-serif;
}
a:hover {
  color: #c62107;
  text-decoration: underline;
}


</style>