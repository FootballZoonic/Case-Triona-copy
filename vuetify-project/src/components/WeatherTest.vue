<template>
    <v-carousel class="weather-carousel mt-4" @change="onSlideChange" hide-delimiters>
        <v-carousel-item v-for="(location, index) in weatherLocations" :key="index">
            <v-card class="weather-card" :style="{ backgroundImage: `url(${location.backgroundImage})` }">
                <v-card-title>
                    <h2 class="text-h4">🌤 Current Weather</h2>
                </v-card-title>
                <v-card-subtitle :style="{ color: 'inherit', opacity: 1 }">
                    <h3 class="text-h5">{{ location.name }}</h3>
                </v-card-subtitle>
                <v-card-text v-if="weather">
                    <p class="text-body-1">🌡 Temperature: {{ weather.temperature }}°C</p>
                    <p class="text-body-1">💨 Wind Speed: {{ weather.windspeed }} km/h</p>
                </v-card-text>
                <v-card-text v-else>
                    Loading weather data...
                </v-card-text>
            </v-card>
        </v-carousel-item>
    </v-carousel>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import sunnyImage from '@/assets/sunny.jpg';
import sunnyImage2 from '@/assets/sunny2.jpg';


interface WeatherData {
    temperature: number;
    windspeed: number;
}

interface WeatherLocation {
    name: string;
    latitude: number;
    longitude: number;
    backgroundImage: string;
}

const weather = ref<WeatherData | null>(null);

const weatherLocations: WeatherLocation[] = [
    {
        name: 'Triona in Borlänge',
        latitude: 60.485344,
        longitude: 15.432495,
        backgroundImage: sunnyImage
    },
    {
        name: 'Triona in Helsinki',
        latitude: 60.171654,
        longitude: 24.946883,
        backgroundImage: sunnyImage2
    }
    // Add more locations as needed
];

const fetchWeather = async (latitude: number, longitude: number) => {
    try {
        const response = await fetch(
            `https://api.open-meteo.com/v1/forecast?latitude=${latitude}&longitude=${longitude}&current_weather=true`
        );
        const data = await response.json();
        weather.value = {
            temperature: data.current_weather.temperature,
            windspeed: data.current_weather.windspeed,
        };
    } catch (error) {
        console.error("Error fetching weather data:", error);
    }
};

const onSlideChange = (index: number) => {
    console.log('Slide changed to index:', index);
    const location = weatherLocations[index];
    fetchWeather(location.latitude, location.longitude);
};

// Fetch data for the first location when component mounts
onMounted(() => fetchWeather(weatherLocations[0].latitude, weatherLocations[0].longitude));
</script>

<style scoped>
.weather-carousel {
    max-width: 300px;
    margin: auto;
}

.weather-card {
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
    text-align: center;
    background-size: cover;
    background-position: center;
    min-height: 300px;
}

.text-h4 {
    display: flex;
    justify-content: center;
    font-family: 'Bebas Neue', sans-serif;
}

.text-h5 {
    display: flex;
    color: #c62107;
    justify-content: center;
    font-weight: bold;
}
</style>