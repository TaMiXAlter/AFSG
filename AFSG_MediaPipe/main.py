from pygrabber.dshow_graph import FilterGraph
import cv2
import mediapipe as mp
import socket
mp_drawing = mp.solutions.drawing_utils          
mp_drawing_styles = mp.solutions.drawing_styles  
mp_pose = mp.solutions.pose                     

mp_modelpath = 'model\pose_landmarker_lite.task'
#WebCam 
graph = FilterGraph()
devices = graph.get_input_devices()
target_name = "OBS Virtual Camera"

print("all webcam devices :")
for i, name in enumerate(devices):
    print(f"Index {i}: {name}")
    
index = 0
if target_name in devices :
    index = devices.index(target_name)  
else:
    print("Cann't get OBS Virtual Camera please check angain and restart")
Camera = cv2.VideoCapture(index)

# Communication
IP = "127.0.0.1"
Port = 6500
sock = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)


#MedePi
with mp_pose.Pose(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as pose:

    if not Camera.isOpened():
        print("Cannot open camera")
        exit()
        
    while True:
        # 取得pose 資料 
        ret, img = Camera.read()
        if not ret:
            print("Cannot receive frame")
            break
        img = cv2.resize(img,(520,300))              
        imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)  
        results = pose.process(imgRGB)             
        
        if not results:
            print("Cannot receive result")
            break      
        
        if results.pose_landmarks is not None:
            # 整理 pose landmark
            pose_landmarks = []
            for lm in results.pose_landmarks.landmark:
                pose_landmarks.extend([lm.x, 1 - lm.y, lm.z])

            # 以 UDP 送出資料
            sock.sendto(str.encode(str(pose_landmarks)), (IP, Port))
        else:
            print("No pose landmarks detected")  # Debug 訊息

        
Camera.release()
cv2.destroyAllWindows()