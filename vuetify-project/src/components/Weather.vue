<template>
  <v-card :class="['weather-card', weatherClass]">
    <v-card-title class="card-title-style">
      🌤 Current Weather
    </v-card-title>
    <v-card-subtitle :class="['card-subtitle-style', `${weatherClass}-subtitle-color`]">
      {{ props.location }} Triona
    </v-card-subtitle>
    <v-card-text class="custom-bg card-body-text-style" v-if="weather">
      <v-paragraph class="card-paragraph">🌡 Temperature: {{ weather.temperature }}°C  </v-paragraph>
      <v-paragraph class="card-paragraph">💨 Wind Speed: {{ weather.windspeed }} km/h </v-paragraph>
    </v-card-text>
    <v-card-text v-else> Loading weather data... </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { ref, onMounted, defineProps } from "vue";

const props = defineProps({
  location: {
    type: String,
    required: true
  },
  latitude: {
    type: String,
    required: true
  },
  longitude: {
    type: String,
    required: true
  }
});

interface WeatherData {
  temperature: number;
  windspeed: number;
}

const weather = ref<WeatherData | null>(null);

const latitude = props.latitude;
const longitude = props.longitude;


const fetchWeather = async () => {
  try {
    const response = await fetch(
      `https://api.open-meteo.com/v1/forecast?latitude=${latitude}&longitude=${longitude}&current_weather=true`
    );
    const data = await response.json();
    weather.value = {
      temperature: data.current_weather.temperature,
      windspeed: data.current_weather.windspeed
    };
  } catch (error) {
    console.error("Error fetching weather data:", error);
  }
};

// Fetch data when component mounts
onMounted(fetchWeather);

const weatherClass = computed(() => {
  if (!weather.value) return "";
  if (weather.value.windspeed > 30) {
    return "windy-weather";
  } else if (weather.value.temperature < 0) {
    return "cold-weather";
  } else if (weather.value.temperature > 10) {
    return "hot-weather";
  } else {
    return "mild-weather";
  }
});
</script>

<style scoped>
.weather-card {
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  margin: auto;
  background-size: cover;
  background-position: center;
  display: block;
}

.hot-weather {
  background-image: url("@/assets/sunny_weather.jpeg");
}

.cold-weather {
  background-image: url("@/assets/cold_weather.jpeg");
}

.mild-weather {
  background-image: url("@/assets/mild_weather.jpeg");
}

.windy-weather {
  background-image: url("@/assets/windy_weather.jpeg");
}

.hot-weather-subtitle-color {
  color: #c62107;
}

.cold-weather-subtitle-color {
  color: #dfd5bb;
}

.mild-weather-subtitle-color {
  color: #c62107;
}

.windy-weather-subtitle-color {
  color: #c62107;
}

.custom-bg {
  background-color: white;
  margin-top: 1rem;
  padding: 0.5rem;
  border-radius: 8px;
  max-width: 50%; /* Justera bredden efter behov */
  margin-left: auto;
  margin-right: auto;
}
</style>
