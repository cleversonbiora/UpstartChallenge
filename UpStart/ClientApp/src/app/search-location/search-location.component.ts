import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search-location',
  templateUrl: './search-location.component.html'
})
export class SearchLocationComponent {

  public showModal:boolean = false;
  public forecasts: ForecastResult[];
  public forecast: ForecastResult;
  public _http: HttpClient;
  public _baseUrl: string = 'https://localhost:44393/';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  public locationApi = `${this._baseUrl}Location?address=:my_own_keyword`;
  public loadingTemplate = '<h1>Loading h1</h1>';

  myModel;
  
  public openDetails(item: ForecastResult){
    this.showModal = true;
    this.forecast = item;
  }
  
  public closeDetails(){
    this.showModal = false;
  }
  
  public myListFormatter(data: any): string {
    return data['matchedAddress'];
  }
  public myValueFormatter(data: any): string {
    return `${data['matchedAddress']}`;
  }
  public getForecast(addr: any): void {
    
    this._http.get<ApiResult>(`${this._baseUrl}Weather?latitude=${addr.latitude}&longitude=${addr.longitude}`).subscribe(result => {
      this.forecasts = result.data;
    }, error => console.error(error));
  }
}

interface ApiResult {
  success:  boolean,
  data: ForecastResult[]
}

interface ForecastResult {
  detailedForecast: string
  endTime: string
  icon: string
  isDaytime: boolean
  name: string
  number: number
  shortForecast: string
  startTime: string
  temperature: number
  temperatureTrend: string
  temperatureUnit: string
  windDirection: string
  windSpeed: string
}