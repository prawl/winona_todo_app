export const env: { env: 'local' | 'dev' | 'qa' | 'prod' } = {
    env: 'local',
  };
  
  
  export interface EnvironmentConfig {
  
    apiUrl: string;
  }
  
  export interface Environments {
    local: EnvironmentConfig;
    dev: EnvironmentConfig;
    qa: EnvironmentConfig;
    prod: EnvironmentConfig;
  }
  
  export const environments: Environments = {
    local: {
      apiUrl: 'https://localhost:7120/api/TodoItems',
    },
    dev: {
      apiUrl: 'https://localhost:7120/api/TodoItems',
    },
    qa: {
      apiUrl: 'https://localhost:7120/api/TodoItems',
    },
    prod: {
      apiUrl: 'https://localhost:7120/api/TodoItems',
    }
  };