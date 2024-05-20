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
    apiUrl: 'http://localhost:5220/api/TodoItems',
  },
  dev: {
    apiUrl: 'http://localhost:5220/api/TodoItems',
  },
  qa: {
    apiUrl: 'http://localhost:5220/api/TodoItems',
  },
  prod: {
    apiUrl: 'http://localhost:5220/api/TodoItems',
  }
};
