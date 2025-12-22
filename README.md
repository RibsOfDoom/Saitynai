Ataskaita, veikia tik prisijungus is KTU:
https://ktuedu-my.sharepoint.com/:w:/g/personal/ignsto_ktu_lt/IQB27-wMBtq_SrzymuTP7c4DAWEpu7ZDDfb8Z4mheq_LMPk?e=hUvNIF
TLDR:

cities
/api/cities GET ALL 200
/api/cities/{name} GET 200, 404
/api/cities POST 200, 400
/api/cities/{name} PUT 200
/api/cities/{name} DELETE 200, 404
bodies
/api/cities/{name}/bodies GET ALL 200
/api/cities/{name}/bodies/{name} GET 200, 404
/api/cities/{name}/bodies POST 200, 400
/api/cities/{name}/bodies/{name} PUT 200, 404
/api/cities/{name}/bodies/{name} DELETE 200, 404
fish
/api/cities/{name}/bodies/{name}/fish GET ALL 200
/api/cities/{name}/bodies/{name}/fish/{id} GET 200, 404
/api/cities/{name}/bodies/{name}/fish POST 200
/api/cities/{name}/bodies/{name}/fish/{id} PUT 200, 404
/api/cities/{name}/bodies/{name}/fish/{id} DELETE 200, 404


description: API for Cities, Bodies, and Fish management

servers:
  - url: http://localhost:5160/api/

paths:
  /cities:
    get:
      summary: Get all cities
      responses:
        '200':
          description: List of cities
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/CityDTO'
    post:
      summary: Create a new city
      requestBody:
        description: City to create
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CreateCityDTO'
      responses:
        '201':
          description: City created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/CityDTO'

  /cities/{name}:
    get:
      summary: Get city by name
      parameters:
        - name: name
          in: path
          required: true
          schema:
            type: string
      responses:
        '200':
          description: City found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/CityDTO'
        '404':
          description: City not found
    put:
      summary: Update city by name
      parameters:
        - name: name
          in: path
          required: true
          schema:
            type: string
      requestBody:
        description: City data to update
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UpdateCityDTO'
      responses:
        '200':
          description: City updated
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/CityDTO'
        '404':
          description: City not found
    delete:
      summary: Delete city by name
      parameters:
        - name: name
          in: path
          required: true
          schema:
            type: string
      responses:
        '204':
          description: City deleted successfully
        '404':
          description: City not found

  /cities/{cityName}/bodies:
    get:
      summary: Get all bodies for a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
      responses:
        '200':
          description: List of bodies
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/BodyDTO'
    post:
      summary: Create a new body for a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
      requestBody:
        description: Body to create
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CreateBodyDTO'
      responses:
        '201':
          description: Body created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/BodyDTO'
        '404':
          description: City not found
        '400':
          description: Body already exists

  /cities/{cityName}/bodies/{bodyName}:
    get:
      summary: Get a body by name in a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
      responses:
        '200':
          description: Body found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/BodyDTO'
        '404':
          description: City or Body not found
    put:
      summary: Update a body in a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
      requestBody:
        description: Body data to update
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UpdateBodyDTO'
      responses:
        '200':
          description: Body updated
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/BodyDTO'
        '404':
          description: City or Body not found
    delete:
      summary: Delete a body in a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
      responses:
        '204':
          description: Body deleted successfully
        '404':
          description: Body not found

  /cities/{cityName}/bodies/{bodyName}/fish:
    get:
      summary: Get all fish in a body of a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
      responses:
        '200':
          description: List of fish
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/FishDTO'
    post:
      summary: Create new fish in a body of a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
      requestBody:
        description: Fish to create
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CreateFishDTO'
      responses:
        '201':
          description: Fish created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/FishDTO'
        '404':
          description: City or Body not found

  /cities/{cityName}/bodies/{bodyName}/fish/{fishId}:
    get:
      summary: Get fish by ID in a body of a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
        - name: fishId
          in: path
          required: true
          schema:
            type: integer
      responses:
        '200':
          description: Fish found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/FishDTO'
        '404':
          description: City, Body, or Fish not found
    put:
      summary: Update fish by ID in a body of a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
      requestBody:
        description: Fish data to update
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UpdateFishDTO'
      responses:
        '200':
          description: Fish updated
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/FishDTO'
        '404':
          description: City, Body, or Fish not found
    delete:
      summary: Delete fish by ID in a body of a city
      parameters:
        - name: cityName
          in: path
          required: true
          schema:
            type: string
        - name: bodyName
          in: path
          required: true
          schema:
            type: string
        - name: fishId
          in: path
          required: true
          schema:
            type: integer
      responses:
        '204':
          description: Fish deleted successfully
        '404':
          description: Fish not found

components:
  schemas:

    CityDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
      required:
        - name

    CreateCityDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
      required:
        - name

    UpdateCityDTO:
      type: object
      properties:
        description:
          type: string
      required:
        - description

    BodyDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
        cityName:
          type: string
      required:
        - name
        - cityName

    CreateBodyDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
      required:
        - name

    UpdateBodyDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
      required:
        - name

    FishDTO:
      type: object
      properties:
        id:
          type: integer
        name:
          type: string
        description:
          type: string
        season:
          type: integer
          description: Season when fish can be caught (1-4)
        timeFrom:
          type: integer
          description: Catching start hour (0-23)
        timeTo:
          type: integer
          description: Catching end hour (0-23)
        bodyName:
          type: string
      required:
        - id
        - name
        - bodyName

    CreateFishDTO:
      type: object
      properties:
        name:
          type: string
        description:
          type: string
        season:
          type: integer
          description: Season when fish can be caught (1-4)
        timeFrom:
          type: integer
          description: Catching start hour (0-23)
        timeTo:
          type: integer
          description: Catching end hour (0-23)
      required:
        - name

    UpdateFishDTO:
      type: object
      properties:
        id:
          type: integer
        name:
          type: string
        description:
          type: string
        season:
          type: integer
          description: Season when fish can be caught (1-4)
        timeFrom:
          type: integer
          description: Catching start hour (0-23)
        timeTo:
          type: integer
          description: Catching end hour (0-23)
      required:
        - id
        - name
